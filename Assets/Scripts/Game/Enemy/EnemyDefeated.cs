using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDefeated : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    private int currentEnemyDefeated = 0;

    public void UpdateEnemyDefeated(int enemeDefeated)
    {
        currentEnemyDefeated = enemeDefeated;
        valueText.text = "MINIBOSS DEFEATED : " + currentEnemyDefeated.ToString();
    }
}
