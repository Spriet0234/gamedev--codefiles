using UnityEngine;
using UnityEngine.SceneManagement; 

public class CureScript : MonoBehaviour
{
    public string endGameSceneName = "UpgradeScene";
    public string cureFound = "CureFound";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Load the end game scene
            if (SceneManager.GetActiveScene().name == "GS3")
            {
                // Load the UpgradeScene
                CoinManager.Instance.ResetOrDestroyOnDeath();
                GameManager.Instance.ResetOrDestroyOnDeath();
                SceneManager.LoadScene(cureFound);
            }
            else
            {
                // Load the CureFound scene
                SceneManager.LoadScene(endGameSceneName);
            }
        }
    }
}
