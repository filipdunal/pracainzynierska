using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
#pragma warning disable 0618 

public class RefreshServer : MonoBehaviour
{
    public Button thisButton;
    public InputField portInputField;

    private void Start()
    {
        portInputField.text = SettingsOfPlayer.lastUsedNetworkPort.ToString();
        thisButton.onClick.AddListener(ChangePort);
    }
    void ChangePort()
    {
        int intToSend;
        if(int.TryParse(portInputField.text, out intToSend))
        {
            SettingsOfPlayer.lastUsedNetworkPort = intToSend;
            NetworkServer.Listen(SettingsOfPlayer.lastUsedNetworkPort);
        }
        else
        {
            //thisButton.GetComponent<Image>().color = Color.red;
        }
        
    }
}
