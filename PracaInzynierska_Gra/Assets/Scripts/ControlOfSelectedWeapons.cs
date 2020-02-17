using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ControlOfSelectedWeapons : MonoBehaviour
{
    public int maxCountOfActiveToggles;
    //ToggleGroup tg;
    Toggle[] toggles;
    SettingsOfUser settingsOfUser;

    private void Start()
    {
        toggles = GetComponentsInChildren<Toggle>();
        settingsOfUser = FindObjectOfType<SettingsOfUser>();
    }
    public void Control(Toggle lastSwitched)
    {
        int count = 0;
        foreach(Toggle toggle in toggles)
        {
            if(toggle.isOn)
            {
                count++;
            }
        }
        if (count > maxCountOfActiveToggles)
        {
            lastSwitched.isOn = false;
        }
    }
    public void TakeSelectedWeapons()
    {
        foreach(Weapon weapon in settingsOfUser.weapons)
        {
            weapon.chosen = false;
        }
        
        foreach(Weapon weapon in settingsOfUser.weapons)
        {
            foreach (Toggle toggle in toggles)
            {
                if(weapon.prefabName==toggle.name && toggle.isOn)
                {
                    weapon.chosen = true;
                    break;
                }
            }
        }
    }
}
