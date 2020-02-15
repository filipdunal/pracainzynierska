using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener( (value) => { TaskOnValueChanged(value); });
    }
    
    void TaskOnValueChanged(bool condition)
    {
        GetComponentInParent<ControlOfSelectedWeapons>().Control(GetComponent<Toggle>());
    }
}
