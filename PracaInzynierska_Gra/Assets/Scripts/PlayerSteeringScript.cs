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

    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;

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
        actionClickedText.text = "Action: "+actionClicked.ToString();
    }

    private void Update()
    {
        //Get values and assign them
        if(NetworkServerUI.receivedString!=null)
        {
            messageValue = NetworkServerUI.receivedString.Split('|');
            x = int.Parse(messageValue[0]);
            y = int.Parse(messageValue[1]);
            z = int.Parse(messageValue[2]);

            sliderX.value = x / (float)360.0;
            sliderY.value = y / (float)360;
            sliderZ.value = z / (float)360;

            actionClicked = int.Parse(messageValue[3]);
        }
    }
}
