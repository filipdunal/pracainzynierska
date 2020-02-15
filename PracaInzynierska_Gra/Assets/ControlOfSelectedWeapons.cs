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

    private void Start()
    {
        toggles = GetComponentsInChildren<Toggle>();
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
        foreach(Toggle toggle in toggles)
        {
            //foreach()
        }
    }
}
