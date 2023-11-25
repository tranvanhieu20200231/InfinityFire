using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public int currentScore = 0;

    public void UpdateScore(int score)
    {
        currentScore += score;
        valueText.text = "SCORE : " + currentScore.ToString();
    }
}
