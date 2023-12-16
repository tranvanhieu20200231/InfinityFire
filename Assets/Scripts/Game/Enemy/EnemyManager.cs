using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public float arena = 20f;
    public GameObject[] enemyPrefab;
    public Transform spawnPoint;
    public GameObject miniBossPrefab;
    public GameObject spawnWarningPrefab;
    public float spawnInterval = 3f;
    public int enemySpawn = 0;
    public int maxEnemies = 10;
    public int scoreSpaw = 200;
    private int enemyIndex = 0;
    public int nextScoreSpaw = 1000;

    public Score score;
    private float nextSpawnTime;
    private bool isSpawningMiniBoss = false;
    // Bug Spawn vao vi tri "Obstacle"
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            if (!isSpawningMiniBoss)
            {
                SpawnWarningAndEnemy();
            }
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (!isSpawningMiniBoss && score.currentScore >= scoreSpaw)
        {
            isSpawningMiniBoss = true;
            SpawnMiniBoss();
        }
    }

    void SpawnWarningAndEnemy()
    {
        for (int i = 0; i < enemySpawn; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                Vector2 randomOffset = Random.insideUnitCircle * arena;
                Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

                GameObject spawnWarning = Instantiate(spawnWarningPrefab, spawnPosition, Quaternion.identity);

                StartCoroutine(SpawnEnemyAfterDelay(spawnWarning, spawnPosition));
            }
        }
    }

    IEnumerator SpawnEnemyAfterDelay(GameObject spawnWarning, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(2f);

        SpawnEnemy(spawnPosition);
    }

    void SpawnEnemy(Vector3 spawnPosition)
    {
        enemyIndex = Random.Range(0, enemyPrefab.Length);
        GameObject enemy = Instantiate(enemyPrefab[enemyIndex], spawnPosition, Quaternion.identity);
    }

    void SpawnMiniBoss()
    {
        Vector2 randomOffset = Random.insideUnitCircle * arena;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

        GameObject spawnWarning = Instantiate(spawnWarningPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnMiniBossAfterDelay(spawnWarning, spawnPosition));
    }

    IEnumerator SpawnMiniBossAfterDelay(GameObject spawnWarning, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(2f);

        scoreSpaw = scoreSpaw + nextScoreSpaw;
        GameObject miniBoss = Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);

        isSpawningMiniBoss = false;
    }
}
