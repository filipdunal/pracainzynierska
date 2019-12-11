using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMainScript : MonoBehaviour
{
    public GameObject covering;
    private void OnGUI()
    {
        if(NetworkClientUI.isConnected)
        {
            covering.SetActive(false);
        }
        else
        {
            covering.SetActive(false);
        }
    }
}
