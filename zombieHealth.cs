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
        // Find the CoinManager in the scene
        CoinManager coinManager = FindObjectOfType<CoinManager>();
        if (coinManager != null)
        {
            coinManager.AddCoins(5); // Assuming you want to add 5 coins. Adjust the value as necessary.
        }
        else
        {
            Debug.LogError("Failed to find CoinManager in the scene.");
        }

        // Destroy the zombie GameObject
        Destroy(gameObject);
    }
}
