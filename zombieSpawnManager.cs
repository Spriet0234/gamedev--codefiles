using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab; 
    public float spawnTime = 2f;
    public int maxZombies = 10; 
    public float minSpawnDistanceFromPlayer = 10f; 
    public float maxSpawnDistanceFromPlayer = 30f; 
    public Transform playerTransform; 

    public float minX = -20f;
    public float maxX = 20f;
    public float minY = -20f;
    public float maxY = 20f;

    private float timer; 

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
