using UnityEngine;

public class zombieHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the zombie GameObject
        CoinManager.Instance.AddCoins(5); 
        Destroy(gameObject);
    }
}
