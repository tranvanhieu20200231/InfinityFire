﻿using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform spawnPoint;
    public GameObject miniBossPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 10;
    public int maxMiniBoss = 1;
    public int scoreSpaw = 200;
    private int enemyIndex = 0;
    private int currentScoreSpaw = 0;
    private int miniBossCount = 0;

    public Score score;
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        SpawnMiniBoss();
    }

    void SpawnEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            Vector2 randomOffset = Random.insideUnitCircle.normalized * 20f;
            Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            enemyIndex = Random.Range(0, enemyPrefab.Length);
            GameObject enemy = Instantiate(enemyPrefab[enemyIndex], spawnPosition, Quaternion.identity);

            currentScoreSpaw = score.currentScore;
        }
    }

    void SpawnMiniBoss()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * 20f;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

        if (currentScoreSpaw >= scoreSpaw && miniBossCount < maxMiniBoss)
        {
            miniBossCount++;
            currentScoreSpaw = 0;
            GameObject miniBoss = Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);
        }
    }

}
