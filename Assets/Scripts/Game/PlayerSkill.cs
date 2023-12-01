using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject shieldPrefab;     // Đối tượng Shield
    private GameObject currentShield;   // Tham chiếu đến Shield hiện tại

    public float ShieldTime;
    public float ShieldCD;

    private float shieldCDT = 0;
    private float shieldTime;
    private bool shieldOnce = false;
    private bool shieldButton = false;

    public CDTimeBar CDTBar;

    private void Start()
    {
        CDTBar.UpdateCDTimeBar(shieldCDT, ShieldCD);
    }

    private void Update()
    {
        Shield();
    }

    public void ShieldButtonClicked()
    {
        shieldButton = true;
    }

    void Shield()
    {
        if ((Input.GetKeyDown(KeyCode.E) || shieldButton) && shieldOnce == false && shieldCDT <= 0)
        {
            Vector3 shieldPos = transform.position + new Vector3 (0, -0.5f, 0);
            currentShield = Instantiate(shieldPrefab, shieldPos, Quaternion.identity);
            currentShield.transform.parent = transform;

            shieldTime = ShieldTime;
            shieldOnce = true;
            shieldCDT = ShieldCD;
            shieldButton = false;
        }
        shieldCDT -= Time.deltaTime;
        CDTBar.UpdateCDTimeBar(shieldCDT, ShieldCD);

        if (shieldTime <= 0 && shieldOnce == true)
        {
            shieldOnce = false;
            Destroy(currentShield);
        }
        else
        {
            shieldTime -= Time.deltaTime;
        }
    }
}
