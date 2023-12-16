using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    WinGameController winGame;

    private void Start()
    {
        winGame = FindObjectOfType<WinGameController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            winGame.WinGame();
        }
    }
}
