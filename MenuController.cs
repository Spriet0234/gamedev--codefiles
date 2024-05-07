using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
