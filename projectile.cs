using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // The damage a projectile deals

    void Start()
    {
        // Destroy the projectile after 1 second
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with a zombie
        zombieHealth zombieHealth = other.GetComponent<zombieHealth>();
        if (zombieHealth != null)
        {
            // Apply damage to the zombie
            zombieHealth.TakeDamage(damage);
        }

        // Destroy the projectile unless it's the player
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
