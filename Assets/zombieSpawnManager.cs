using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab; 
    public float spawnTime = 2f;
    public int maxZombies = 10; 
    public float minSpawnDistanceFromPlayer = 10f; 
    public float maxSpawnDistanceFromPlayer = 30f; 
    public Transform playerTransform; 
    

   
    float minX;
    float maxX;
    float minY;
    float maxY; 

    

    private float timer; 
    void Start()
    {
        SetBoundsBasedOnScene();
    }

    private void SetBoundsBasedOnScene()
    {
 string sceneName = SceneManager.GetActiveScene().name;
    switch (sceneName)
        {
            case "GameScene":

                minX = -20f; 
                maxX = 20f; 
                minY = -20f; 
                maxY = 20f;
                break;
            case "GS2":
                minX = -25f; 
                maxX = 40f; 
                minY = -17f; 
                maxY = 20f;
                break;
            
            default:
                minX = -25f; 
                maxX = 23f; 
                minY = -28f; 
                maxY = 18f;
                break;
        }
}

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime && GameObject.FindGameObjectsWithTag("Zombie").Length < maxZombies)
        {
            SpawnZombie();
            timer = 0;
        }
    }

    void SpawnZombie()
{
    Vector2 spawnPosition = Vector2.zero;
    bool validPosition = false;
    float checkRadius = 1f; 

    while (!validPosition)
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        spawnPosition = new Vector2(x, y);

        if (Vector2.Distance(playerTransform.position, spawnPosition) >= minSpawnDistanceFromPlayer && Vector2.Distance(playerTransform.position, spawnPosition) <= maxSpawnDistanceFromPlayer)
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(spawnPosition, checkRadius);

            if (hitCollider == null)
            {
                validPosition = true;
            }
        }
    }

    Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
}

}
