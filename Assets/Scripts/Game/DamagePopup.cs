using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DamagePopup : MonoBehaviour
{
    public TextMeshPro valueText;

    public void UpdateText(int damage)
    {
        valueText.text = damage.ToString();
    }
}
