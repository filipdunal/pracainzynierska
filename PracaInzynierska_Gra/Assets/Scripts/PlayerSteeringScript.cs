using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSteeringScript : MonoBehaviour
{
    public Text debugReceivedData;
    string[] messageValue;

    float x;
    float y;
    float z;
    private void OnGUI()
    {
        debugReceivedData.text = NetworkServerUI.receivedString;
    }

    private void Update()
    {
        messageValue = NetworkServerUI.receivedString.Split('|');
        //0-x
        //1-y
        //2-z
        //3-actionPressed

        x = float.Parse(messageValue[0]);
        y = float.Parse(messageValue[1]);
        z = float.Parse(messageValue[2]);
    }
}
