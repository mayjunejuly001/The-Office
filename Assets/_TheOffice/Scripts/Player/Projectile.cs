using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f,0);

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f); // Destroys after 5 seconds
    }

    


    private void Start()
    {
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Prevent hitting the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Laptop ignored the player!");
            return; // Do nothing if it hits the player
        }

        Damageable damageable = collision.GetComponent<Damageable>();

        Debug.Log(damageable ? damageable.name + " found" : "No damageable component detected.");

        if (damageable != null) // Only call Hit if a Damageable component is found
        {
            damageable.Hit(damage);
            Debug.Log(collision.name + " hit for " + damage);
        }

        // Make sure we're destroying the projectile, NOT the player
        Destroy(gameObject); // `this.gameObject` ensures we're destroying the object this script is attached to
    }


}
