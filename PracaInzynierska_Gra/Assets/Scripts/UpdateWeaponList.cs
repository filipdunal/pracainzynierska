using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateWeaponList : MonoBehaviour
{
    void Start()
    {
        SettingsOfUser settingsOfUser = FindObjectOfType<SettingsOfUser>();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in transform)
        {
            foreach (Weapon w in settingsOfUser.weapons)
            {
                child.gameObject.SetActive(child.gameObject.activeSelf ||((w.prefabName == child.name) && w.unlocked));
            }
        }
    }
}
