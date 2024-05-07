using UnityEngine;
using TMPro; // Add this namespace to use TextMeshPro
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    public int Coins { get; private set; }

    [SerializeField] private TextMeshProUGUI coinsText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the CoinManager across scenes
            SceneManager.sceneLoaded += OnSceneLoaded;  
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject coinTextObj = GameObject.FindGameObjectWithTag("CoinsText");
        if (coinTextObj != null) // Check if the GameObject was found
        {
            coinsText = coinTextObj.GetComponent<TextMeshProUGUI>();
            if (coinsText != null) // Check if the component was found
            {
                UpdateCoinDisplay();  // Refresh the display whenever a new scene is loaded
            }
        }
           
    }
    public void ResetOrDestroyOnDeath()
{
    Destroy(gameObject);

  
}

    public void AddCoins(int amount)
    {
        Coins += amount;
        UpdateCoinDisplay();
    }
    public void RemCoins(int amount)
    {
        Coins -= amount;
        UpdateCoinDisplay();
    }

    public void ResetCoins()
    {
        Coins = 0;
        UpdateCoinDisplay();  // Update display to show zero coins
    }
   

    private void UpdateCoinDisplay()
    {
        if (coinsText != null) 
        {
            coinsText.text = $"Money: {Coins}";
        }
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }
}
