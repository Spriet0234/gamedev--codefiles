using UnityEngine;
using TMPro; // Add this namespace to use TextMeshPro

public class CoinManager : MonoBehaviour
{
    public int Coins { get; private set; }

    [SerializeField] private TextMeshProUGUI coinsText; // Reference to your TextMeshPro UI element

    private void Awake()
    {
        // Remove the singleton pattern logic that prevents destruction
        // This instance of CoinManager will be destroyed when the scene changes
        
        // Ensure the coinsText reference is assigned correctly
        // You might still need to do this in the Inspector or programmatically at the start of each scene
        if (coinsText == null)
        {
            Debug.LogError("CoinManager: coinsText reference is not set.");
        }
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        if (coinsText != null)
            coinsText.text = $"Money: {Coins}";
        else
            Debug.LogError("CoinManager: No reference to the coin TextMeshProUGUI component!");
    }
}