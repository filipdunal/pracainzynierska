using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSteeringScript : MonoBehaviour
{
    public Text xText;
    public Text yText;
    public Text zText;
    public Text actionClickedText;
    string[] messageValue;

    int x;
    int y;
    int z;
    int actionClicked;
    int actionClickedCount;
    private void Start()
    {
        actionClicked = 0;
        actionClickedCount = 0;
    }
    private void OnGUI()
    {
        xText.text = "X: "+x.ToString();
        yText.text = "Y: "+y.ToString();
        zText.text = "Z: "+z.ToString();
        //actionClickedText.text = "Action: "+actionClickedCount;
        actionClickedText.text = "Action: "+actionClicked.ToString();
    }

    private void Update()
    {
        //Get values and assign them
        messageValue = NetworkServerUI.receivedString.Split('|');
        x = int.Parse(messageValue[0]);
        y = int.Parse(messageValue[1]);
        z = int.Parse(messageValue[2]);
        actionClicked = int.Parse(messageValue[3]);
        //if ((int)messageValue[3][0]==(int)'1') actionClickedText.text="XD";
        
        //x = x - xOffset;

    }
}
