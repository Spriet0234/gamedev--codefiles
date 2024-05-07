using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Make sure to include this for scene management


public class EndGameScript : MonoBehaviour
{
    public void MenuReturn()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your game scene name
    }
}
