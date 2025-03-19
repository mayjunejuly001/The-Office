using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, int> healthChanged;

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
            healthChanged?.Invoke(_health, MaxHealth);

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

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health <  MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualheal = Mathf.Min(maxHeal, healthRestore);

            Health += actualheal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualheal);
            return true;
        }
        return false;
    }

    //public void Heal(int healthRestore)
    //{
    //    if (!IsAlive) return; // No healing if the character is dead

    //    int healedAmount = MaxHealth - Health; // Calculate how much is restored
    //    if (healedAmount > 0)
    //    {
    //        Health = MaxHealth; // Fully restore health
    //        CharacterEvents.characterHealed.Invoke(gameObject, healedAmount);
    //    }
    //}

}
