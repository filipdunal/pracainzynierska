using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
#pragma warning disable 0618 

public class ShowStatusOfConnection : MonoBehaviour
{
    public Button thisButton;
    public Text buttonText;
    Image buttonImage;
    private void Start()
    {
        buttonImage = thisButton.GetComponent<Image>();
    }
    private void OnGUI()
    {
        string ip = NetworkServerUI.LocalIPAddress();

        //Komputer nie jest podlaczony do sieci(ip 127...) - czerwony
        if(!NetworkServer.active)
        {
            buttonImage.color = Color.red;
            buttonText.text = "Not connected to network";
            return;
        }

        //Komputer podlaczony do sieci ale bez podlaczonego telefonu - zolty
        if(!NetworkServer.localClientActive)
        {
            buttonImage.color = Color.yellow;
            buttonText.text = "No client connected";
            return;
        }
        //Komputer polaczony z telefonem
        buttonImage.color = Color.green;
        buttonText.text = "Client connected";
    }
}
