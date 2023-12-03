using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyDestroy : MonoBehaviour
{
    WinGameController winGame;

    private void Start()
    {
        winGame = FindObjectOfType<WinGameController>();
    }

    void OnDestroy()
    {
        if (winGame != null)
        {
            winGame.EnemyDefeated();
        }
    }
}
