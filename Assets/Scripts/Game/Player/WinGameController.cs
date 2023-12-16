using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameController : MonoBehaviour
{
    public GameObject youWin;
    public GameObject finishPoint;
    private bool hasSpawnedFinishPoint = false;
    public int requiredPoints = 100;
    public int requiredEnemyDefeated = 1;
    int enemyDefeatedCount = 0;
    public int requiredBossDefeated = 0;
    int bossDefeatedCount = 0;

    Score score;
    EnemyDefeated enemyDefeated;

    private void Start()
    {
        score = FindObjectOfType<Score>();
        enemyDefeated = FindObjectOfType<EnemyDefeated>();
    }

    void Update()
    {
        if (score.currentScore >= requiredPoints && enemyDefeatedCount >= requiredEnemyDefeated && bossDefeatedCount >= requiredBossDefeated)
        {
            SpawnFinishPoint();
        }
    }

    public void EnemyDefeated()
    {
        enemyDefeatedCount++;
        enemyDefeated.UpdateEnemyDefeated(enemyDefeatedCount);
    }

    public void BossDefeated()
    {
        bossDefeatedCount++;
    }

    void SpawnFinishPoint()
    {
        if (finishPoint != null && !hasSpawnedFinishPoint)
        {
            AllEnemyDestroy();
            GameObject mapObject = GameObject.Find("Map");
            Vector3 midMap = mapObject.transform.position;
            Instantiate(finishPoint, midMap, Quaternion.identity);
            hasSpawnedFinishPoint = true;
        }
    }

    public void WinGame()
    {
        youWin.SetActive(true);
        Time.timeScale = 0;
        //UnlockNewLevel();
    }

    void AllEnemyDestroy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
