using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class Attack : MonoBehaviour
{

    [SerializeField]
    public int attackDamage = 10;

   

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        
        Debug.Log(damageable ? damageable.name + " found" : "No damageable component detected.");

        if (collision != null)
        {
            damageable.Hit(attackDamage);

            Debug.Log(collision.name + "hit for " + attackDamage);
        }
    }
    
}
