using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;

    void Start()
    {
        currentWeaponIndex = 0;
        SwitchWeapon();
    }

    void Update()
    {
        // Lua chon vu khi key
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeaponIndex = 2;
            SwitchWeapon();
        }

        // Lua chon vu khi roll
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0f)
        {
            // Roll up
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            SwitchWeapon();
        }
        else if (scrollWheel < 0f)
        {
            // Roll down
            currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        // Chuyen doi vu khi
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        weapons[currentWeaponIndex].SetActive(true);
    }
}
