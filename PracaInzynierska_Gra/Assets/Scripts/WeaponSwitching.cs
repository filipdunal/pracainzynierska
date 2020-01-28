using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    int selectedWeapon = 0;
    private void Start()
    {
        SelectWeapon();
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(selectedWeapon==i)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
    
    public void NextWeapon()
    {
        if(selectedWeapon>=transform.childCount-1)
        {
            selectedWeapon=0;
        }
        else
        {
            selectedWeapon++;
        }
        SelectWeapon();
    }
    public void PreviousWeapon()
    {
        if(selectedWeapon<=0)
        {
            selectedWeapon = transform.childCount - 1;
        }
        else
        {
            selectedWeapon--;
        }
        SelectWeapon();
    }
    public void Shot()
    {
        transform.GetChild(selectedWeapon).GetComponent<WeaponScript>().Shot();
    }
    
    
}
