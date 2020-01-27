using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSteeringScript : MonoBehaviour
{
    string[] messageValue;

    //Received values
    float x = 0f;
    float y = 0f;

    public float lerpStrength = 0.5f;
    public Vector2 mousePosition;
    public bool shotClick;
    public bool pauseClick;

    OneSteeringScript listOfActivities;

    private void Start()
    {
        mousePosition = new Vector2(0f, 0f);
        listOfActivities = GetComponent<OneSteeringScript>();
    }
    private void Update()
    {
        //Get values and assign them
        if (NetworkServerUI.receivedString != null)
        {
            messageValue = NetworkServerUI.receivedString.Split('|');
            //0 - x
            //1 - y
            //2 - shot (1:0)
            //3 - pause (1:0)

            // Optimize this block of code in future
            if (shotClick)                   
            {
                if(messageValue[2] == "1") //returns true every frame when button is held down 
                {
                    listOfActivities.ShotManually();
                }
                else
                {
                    listOfActivities.ReleaseShotManually(); //returns true in frame when shot button is released
                }
            }
            if(!pauseClick && ((messageValue[3] == "1")))
            {
                listOfActivities.DoPause();
            }
            
            x = float.Parse(messageValue[0], System.Globalization.CultureInfo.InvariantCulture);
            y = float.Parse(messageValue[1], System.Globalization.CultureInfo.InvariantCulture);
            shotClick = (messageValue[2] == "1");
            pauseClick = (messageValue[3] == "1");
            
            Aim();
        }
    }
    void Aim()
    {
        Vector2 v = new Vector2();

        v.x = Screen.width * x / 100;
        v.y = Screen.height * y / 100;

        mousePosition= Vector2.Lerp(mousePosition, v, lerpStrength);
    }
}
