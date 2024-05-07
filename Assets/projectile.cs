using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // The damage a projectile deals

    void Start()
    {
        Destroy(gameObject, 1f);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        zombieHealth zombieHealth = other.GetComponent<zombieHealth>();
        if (zombieHealth != null)
        {
            zombieHealth.TakeDamage(GameManager.Instance.ProjectileDamage);
        }

        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
