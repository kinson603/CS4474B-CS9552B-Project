using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 spawnArea = new Vector3(10, 2, 10);
    public float spawnRate;
    
    private float nextSpawn = 0f;
    private int currentLevel;

    void Update()
    {
        GameController gameController = GameController.instance;
        LevelScoreController scoreController = LevelScoreController.Instance;
        currentLevel = scoreController.currentLevel;
        if (gameController == null || gameController.gameState != GameState.Normal) return;
        if (Time.time > nextSpawn)
        {
            float calculatedRate = Mathf.Max(1f,spawnRate - (currentLevel - 1) * 0.2f);
            nextSpawn = Time.time + calculatedRate;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );
        GameObject zombie = Instantiate(enemyPrefab, transform.position + randomPos, Quaternion.identity);
        zombie.GetComponent<Zombie>().SetStatByLevel(currentLevel);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }

}
