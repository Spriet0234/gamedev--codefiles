using UnityEngine;
using UnityEngine.SceneManagement; // Make sure to include this for scene management

public class CureScript : MonoBehaviour
{
    // Specify the name of your end game scene here
    public string endGameSceneName = "CureFound";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we collided with is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Load the end game scene
            SceneManager.LoadScene(endGameSceneName);
        }
    }
}
