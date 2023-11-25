using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBar : MonoBehaviour
{
    public Image fillBar;

    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
    }
}
