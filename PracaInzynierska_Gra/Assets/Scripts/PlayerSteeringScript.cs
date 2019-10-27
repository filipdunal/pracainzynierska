using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSteeringScript : MonoBehaviour
{
    public Text debugReceivedData;
    private void OnGUI()
    {
        debugReceivedData.text = NetworkServerUI.receivedString;
    }
}
