using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyTypeOnePrefab;
    public GameObject enemyTypeTwoPrefab;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int initialSpawnCount = 5;
    [SerializeField] private Vector2 spawnRangeX = new Vector2(-10, 10);
    [SerializeField] private Vector2 spawnRangeZ = new Vector2(-10, 10);

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        // Spawn 5 enemies at game start
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnEnemy();
        }

        // Start spawning new enemies every 2 seconds
        StartCoroutine(SpawnEnemyRoutine());
    }

    void SpawnEnemy()
    {
        Vector3 randomSpawnPos = GenerateRandomPosition();

        GameObject enemyPrefab = (Random.value > 0.5f) ? enemyTypeOnePrefab : enemyTypeTwoPrefab;
        GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPos, Quaternion.identity);

        activeEnemies.Add(newEnemy);
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
        return new Vector3(randomX, 0.5f, randomZ);
    }
}

