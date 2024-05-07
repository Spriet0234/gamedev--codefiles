
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour


{
    public static GameManager Instance { get; private set; }

    private Dictionary<string, string> nextLevelMap;
    public string CurrentScene { get; private set; }
    public string PreviousScene { get; private set; }

    public int ProjectileDamage { get; set; } = 10;  // Default damage
    public float ShootingCooldown { get; set; } = .5f;  // Default cooldown in seconds
    public int Health { get; set; } = 100;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI fireText;


   
    private void Awake()

    {
        InitializeLevelMap();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeLevelMap()
    {
        nextLevelMap = new Dictionary<string, string>
        {
            { "GameScene", "GS2" },
            { "GS2", "GS3" },   
            {"GS3","CureFound"}        
        };
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetOrDestroyOnDeath()
{
    Destroy(gameObject);

  
}

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)

    {
        
        if (scene.name == "UpgradeScene"){
            PreviousScene = CurrentScene;
        }else{
            CurrentScene = scene.name;
        }
        


        GameObject damageTextObj = GameObject.FindGameObjectWithTag("DamageText");
        GameObject fireTextObj = GameObject.FindGameObjectWithTag("FireText");

        if (damageTextObj != null) 
        {
            damageText = damageTextObj.GetComponent<TextMeshProUGUI>();
            if (damageText != null) 
            {
                UpdateUI();  
            }
        }

      

        if (fireTextObj != null) 
        {
            fireText = fireTextObj.GetComponent<TextMeshProUGUI>();
            if (fireText != null) 
            {
                UpdateUI();  
            }
        }
        



        Button damageButton = GameObject.FindGameObjectWithTag("damageButton")?.GetComponent<Button>();
        if (damageButton != null)
        {
            damageButton.onClick.AddListener(UpgradeDamage);
        }
        Button healthButton = GameObject.FindGameObjectWithTag("healthup")?.GetComponent<Button>();
        if (healthButton != null)
        {
            healthButton.onClick.AddListener(IncreaseHealth);
        }else{
             Debug.LogError("Health button not found or missing Button component");
        }
        Button nextButton = GameObject.FindGameObjectWithTag("nextlvl")?.GetComponent<Button>();
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(LoadNextLevel);
        }else{
            Debug.Log("Load next not found");
        }

        
        

    }

     private void UpdateUI()
    {
        if (damageText != null)
            damageText.text = $"Damage: {ProjectileDamage}";
        if (fireText != null)
            fireText.text = $"Health: {Health}";
        
    }
    


    public void UpgradeDamage()
    {
        Debug.Log("pressed");
        Debug.Log(CoinManager.Instance.Coins);
        int increaseAmount = 5;
        int cost = 5;
        if (CoinManager.Instance.Coins >= cost)
        {
            ProjectileDamage += increaseAmount;
            CoinManager.Instance.RemCoins(5);
            Debug.Log($"Damage upgraded to: {ProjectileDamage}");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade damage.");
        }
        UpdateUI();
    }

    public void IncreaseHealth()
    
    {
        Debug.Log("pressed");
        int amt = 25;
        int cost = 5;
        if (CoinManager.Instance.Coins >= cost )
        {
            CoinManager.Instance.RemCoins(5);
            Health += amt;
            Debug.Log($"health increased to ${Health}");
        }
        else
        {
            Debug.Log("Not enough coins.");
        }
        UpdateUI();
    }

    public void LoadNextLevel()
    {

        if (nextLevelMap.ContainsKey(CurrentScene))
        {
            SceneManager.LoadScene(nextLevelMap[CurrentScene]);
        }
        else
        {
            Debug.LogError("Next level not mapped for: " + CurrentScene);
        }
    }
    private void OnDestroy()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}
}
