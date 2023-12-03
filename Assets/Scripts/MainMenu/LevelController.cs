using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Button[] buttons;
    // Loi khong update map
    // => Open full map
    //private void Awake()
    //{
    //    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    //    for (int i = 0; i < buttons.Length; i++)
    //    {
    //        buttons[i].interactable = false;
    //    }
    //    for (int i = 0;i < unlockedLevel; i++)
    //    {
    //        buttons[i].interactable = true;
    //    }
    //}

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
