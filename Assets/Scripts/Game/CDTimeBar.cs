using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CDTimeBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    public void UpdateCDTimeBar(float currentCDTime, float maxCDTime)
    {
        if (currentCDTime > 0)
        {
            fillBar.fillAmount = currentCDTime / maxCDTime;
            string formattedCDTime = currentCDTime.ToString("F1");
            valueText.text = formattedCDTime;
        }
        else
        {
            fillBar.fillAmount = 0;
            valueText.text = string.Empty;
        }
    }
}
