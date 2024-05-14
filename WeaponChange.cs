using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public List<GameObject> weapons;//weapon list
    ShootController shoot;
    private int currentWeaponIndex = 0;

    void Start()
    {
        //default weapon
        SwitchWeapon(currentWeaponIndex);
    }
    private void Update()
    {
        Control();    
    }
    void Control()
    {
        //mouse tekerlegi ile silah degistir
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            //up
            if (scroll > 0f)
            {
                SwitchWeapon((currentWeaponIndex + 1) % weapons.Count);
            }
            //down
            else if (scroll < 0f)
            {
                SwitchWeapon((currentWeaponIndex - 1 + weapons.Count) % weapons.Count);
            }
        }
    }
    public void WeaponUp()
    {
        SwitchWeapon((currentWeaponIndex + 1) % weapons.Count);
    }
    public void WeaponDown()
    {
        SwitchWeapon((currentWeaponIndex - 1 + weapons.Count) % weapons.Count);
    }
    void SwitchWeapon(int index)//silah guncelle
    {
        //eski silahi devre disi birak
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        //yeni silahi etkinlestir
        weapons[index].SetActive(true);

        //index guncelle
        currentWeaponIndex = index;
    }
}