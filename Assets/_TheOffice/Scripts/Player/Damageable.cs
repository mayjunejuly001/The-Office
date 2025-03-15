using System;
using Unity.VisualScripting;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    private Damageable damageable;
    [SerializeField]
    public int _maxHealth = 100;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; } 
    }

    [SerializeField]
    public int _health = 100;

    public bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            
            animator.SetBool(AnimationStrings.isAlive, value);
            
            Debug.Log("IsAlive set to " + value);
        }
    }

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value; // Clamping Health
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    public bool isInvincible = false;
    public float timeSinceHit = 0;
    public float isInvincibilityTime = 0.25f;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

    }

    public void Update()
    {
        if (isInvincible)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit >= isInvincibilityTime) // Changed to >=
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
        }
       
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationStrings.onHit);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

        }
        


    }
}
