using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    int selectedWeapon = 0;

    //Coroutine is used here because weapons gameobjects have to be selected only after deleting not used ones
    private void Start()
    {
        StartCoroutine(DelayedSelectWeapon());
    }

    IEnumerator DelayedSelectWeapon()
    {
        yield return new WaitUntil(() => GetComponent<LoadChosenWeapons>().notChosenWeaponsDeleted);
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
        if (Time.timeScale > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
            SelectWeapon();
        }
        
    }
    public void PreviousWeapon()
    {
        if (Time.timeScale > 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
            SelectWeapon();
        }
        
    }
    public void Shot()
    {
        transform.GetChild(selectedWeapon).GetComponent<WeaponScript>().Shot();
    }
    private void Update()
    {
    }


}
