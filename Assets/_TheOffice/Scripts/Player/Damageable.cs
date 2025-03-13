using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private float _maxHealth = 100f;


    public float MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private float _health = 100f;


    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    public bool _isAlive = true;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {

            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive , value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


}
