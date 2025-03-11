using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackablelayer;
    [SerializeField] private float  damageAmount = 1f;

    [SerializeField] private float timebtwAttack = 0.15f;


    public bool ShouldBedamaging { get; private set; } = false;

    private List<IDamageable> iDamageables = new List<IDamageable>();

    private RaycastHit2D[] hits;

    private Animator animator;

    private float attackTimeCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackTimeCounter = timebtwAttack;
    }

    private void Update()
    {
        if (InputManager.Attack && attackTimeCounter >= timebtwAttack)
        {
            attackTimeCounter = 0f;
            //Attack();
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }

        attackTimeCounter += Time.deltaTime;
    }

    //private void Attack()
    //{
    //    hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0, attackablelayer);

    //    for (int i = 0; i < hits.Length ; i++)
    //    {
    //        EnemyHealth enemyHealth = hits[i].collider.gameObject.GetComponent<EnemyHealth>();

    //        if (enemyHealth != null)
    //        {
    //            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

    //            if (iDamageable != null)
    //            {
    //                iDamageable.Damage(damageAmount);
    //            }
    //        }
    //    }
    //}

    public IEnumerator DamageWhileSlashIsActive()
    {
        ShouldBedamaging = true;


        while (ShouldBedamaging)
        {
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0, attackablelayer);

            for (int i = 0; i < hits.Length; i++)
            {
            
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                    if (iDamageable != null && !iDamageables.Contains(iDamageable))
                    {
                    iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
                    }
                }
                yield return null;
            
        }
        ReturnAttackablesToDamageables();
           }

    private void ReturnAttackablesToDamageables()
    {
        foreach (IDamageable thingThatWasDamaged in iDamageables)
        {
            thingThatWasDamaged.HasTakenDamage = false;
        }
        iDamageables.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    #region animation triggers
    public void ShouldbeDamagingToTrue()
    {
        ShouldBedamaging = true;
    }
    public void ShouldbeDamagingToFalse()
    {
        ShouldBedamaging = false;
    }
    #endregion
}
