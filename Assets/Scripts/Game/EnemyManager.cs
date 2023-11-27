using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform spawnPoint;
    public GameObject miniBossPrefab;
    public GameObject spawnWarningPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 10;
    public int scoreSpaw = 200;
    private int enemyIndex = 0;

    public Score score;
    private float nextSpawnTime;
    // Bug Spawn vao vi tri "Obstacle"
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnWarningAndEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        SpawnMiniBoss();
    }

    void SpawnWarningAndEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 20f;
            Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            GameObject spawnWarning = Instantiate(spawnWarningPrefab, spawnPosition, Quaternion.identity);

            StartCoroutine(SpawnEnemyAfterDelay(spawnWarning, spawnPosition));
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
        Vector2 randomOffset = Random.insideUnitCircle * 20f;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

        if (score.currentScore >= scoreSpaw)
        {
            GameObject spawnWarning = Instantiate(spawnWarningPrefab, spawnPosition, Quaternion.identity);

            StartCoroutine(SpawnMiniBossAfterDelay(spawnWarning, spawnPosition));
        }
    }

    IEnumerator SpawnMiniBossAfterDelay(GameObject spawnWarning, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(2f);

        scoreSpaw = scoreSpaw + 1000;
        GameObject miniBoss = Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);
    }
}
