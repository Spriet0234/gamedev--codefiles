using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public HealthBar healthSlider;
    private List<GameObject> zombiesInContact = new List<GameObject>();

    void Start()
    {
        maxHealth = GameManager.Instance.Health;
        currentHealth = GameManager.Instance.Health;
        healthSlider.setMaxHealth(maxHealth);
        healthSlider.setHealth(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie") && !zombiesInContact.Contains(collision.gameObject))
        {
            zombiesInContact.Add(collision.gameObject);
            StartCoroutine(DamageFromZombie(collision.gameObject));
        }
    }

    IEnumerator DamageFromZombie(GameObject zombie)
{
    while (zombiesInContact.Contains(zombie))
    {
        if (zombie == null || !zombie.activeInHierarchy)
        {
            zombiesInContact.Remove(zombie);
            yield break; // Stop the coroutine if the zombie is no longer active
        }

        TakeDamage(20);
        yield return new WaitForSeconds(1.0f);
    }
}


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            zombiesInContact.Remove(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
{
    CoinManager.Instance.ResetOrDestroyOnDeath();
    GameManager.Instance.ResetOrDestroyOnDeath();
    Debug.Log("Game Over!");
    // Load the main menu scene
    SceneManager.LoadScene("EndGame");
}

}
