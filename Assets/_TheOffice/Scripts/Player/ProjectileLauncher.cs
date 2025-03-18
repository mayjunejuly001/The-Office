using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float projectileSpeed = 10f; // Speed of the projectile
    public float spinSpeed = 360f; // Degrees per second


   
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            launchPoint.position,
            Quaternion.identity
        );

        // Get the facing direction based on Y rotation (0° = right, 180° = left)
        float direction = GetFacingDirection();

        Debug.Log($"Projectile moving in direction: {direction}"); // Debugging

        // Apply correct flipping
        projectile.transform.localScale = new Vector3(
            Mathf.Abs(projectile.transform.localScale.x) * direction, // Ensure proper flipping
            projectile.transform.localScale.y,
            projectile.transform.localScale.z
        );

        // Add velocity if the projectile has a Rigidbody2D
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction * projectileSpeed, 0);
            rb.angularVelocity = spinSpeed * -direction; // Make it spin in the right direction!
        }
    }

    private float GetFacingDirection()
    {
        Transform playerTransform = GetComponentInParent<Transform>();

        if (playerTransform != null)
        {
            return playerTransform.eulerAngles.y == 180 ? -1 : 1;
        }

        return 1; // Default to right if no parent found
    }
}
