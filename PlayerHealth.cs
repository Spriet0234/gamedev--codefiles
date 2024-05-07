using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthSlider;
    private List<GameObject> zombiesInContact = new List<GameObject>();

    void Start()
    {
        currentHealth = maxHealth;
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
            TakeDamage(20); // Adjust the damage as necessary
            yield return new WaitForSeconds(1.0f); // Wait for 1 second before applying damage again
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
    Debug.Log("Game Over!");
    // Load the main menu scene
    SceneManager.LoadScene("EndGame");
}

}
