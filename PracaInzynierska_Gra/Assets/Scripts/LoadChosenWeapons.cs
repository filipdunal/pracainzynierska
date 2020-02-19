using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChosenWeapons : MonoBehaviour
{
    //Coroutine is used here because SettingsOfUser is not fully loaded during first frame of this scene.
    //WaitingForEndOfFrame makes us sure that we called function LoadWeapons as last one.

    public bool giveMeAllWeapons;
    [HideInInspector] public bool notChosenWeaponsDeleted;

    void Start()
    {
        if(!giveMeAllWeapons)
        {
            StartCoroutine(LoadWeapons());
        }
        
    }
    
    IEnumerator LoadWeapons()
    {
        yield return new WaitForEndOfFrame();
        SettingsOfUser settingsOfuser = FindObjectOfType<SettingsOfUser>();
        foreach (Transform child in transform)
        {
            foreach (Weapon weapon in settingsOfuser.weapons)
            {
                if ((child.name == weapon.prefabName) && !weapon.chosen)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        notChosenWeaponsDeleted = true;
    }
}
