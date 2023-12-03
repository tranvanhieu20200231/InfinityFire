using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameController : MonoBehaviour
{
    [SerializeField] GameObject youWin;
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
            WinGame();
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

    void WinGame()
    {
        youWin.SetActive(true);
        Time.timeScale = 0;
        //UnlockNewLevel();
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
