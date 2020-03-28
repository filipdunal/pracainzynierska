using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateWeaponList : MonoBehaviour
{
    GridLayoutGroup gridLayoutGroup;
    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
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

    private void Update()
    {
        gridLayoutGroup.padding.left= (int)gridLayoutGroup.spacing.x;
        gridLayoutGroup.padding.top = (int)gridLayoutGroup.spacing.y;
    }
}
