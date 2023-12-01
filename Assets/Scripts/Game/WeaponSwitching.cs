using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;
    private bool Weapon1 = false;
    private bool Weapon2 = false;
    private bool Weapon3 = false;

    void Start()
    {
        currentWeaponIndex = 0;
        SwitchWeapon();
    }

    void Update()
    {
        // Lua chon vu khi key
        if (Input.GetKeyDown(KeyCode.Alpha1) || Weapon1)
        {
            currentWeaponIndex = 0;
            Weapon1 = false;
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Weapon2)
        {
            currentWeaponIndex = 1;
            Weapon2 = false;
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Weapon3)
        {
            currentWeaponIndex = 2;
            Weapon3 = false;
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

    public void Weapon1ButtonClicked()
    {
        Weapon1 = true;
    }

    public void Weapon2ButtonClicked()
    {
        Weapon2 = true;
    }

    public void Weapon3ButtonClicked()
    {
        Weapon3 = true;
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
