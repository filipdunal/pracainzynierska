using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInputMode : MonoBehaviour
{
    CustomInputModule customInputModule;
    Text displayedInput;
    private void Start()
    {
        displayedInput = GetComponent<Text>();
        customInputModule = GameObject.Find("Custom EventSystem").GetComponent<CustomInputModule>();
    }

    private void OnGUI()
    {
        if(customInputModule.controller==CustomInputModule.Controller.gamepad)
        {
            displayedInput.text = "Gamepad mode";
        }
        else if(customInputModule.controller == CustomInputModule.Controller.mouse)
        {
            displayedInput.text = "Mouse mode";
        }
        else if(customInputModule.controller == CustomInputModule.Controller.phone)
        {
            displayedInput.text = "Phone mode";
        }
        else
        {
            displayedInput.text = "??? mode";
        }
        
    }
}
