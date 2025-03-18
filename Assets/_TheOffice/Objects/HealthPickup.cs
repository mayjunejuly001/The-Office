using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int healthrestore = 100;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthrestore);

            if (wasHealed)
            {
                
            Destroy(gameObject);
            }
        }
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
