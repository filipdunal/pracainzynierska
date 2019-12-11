using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSteeringScript : MonoBehaviour
{
    string[] messageValue;

    //Received values
    float x = 0f;
    float y = 0f;
    int countShots = 0;
    int countPauses = 0;
    int powerOfShot = 0;

    public float lerpStrength = 0.5f;
    public Vector2 mousePosition;

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
            //2 - number of shots
            //3 - number of pauses
            //4 - power of shot

            if(countShots!= int.Parse(messageValue[2]))
            {
                listOfActivities.Shot();
            }
            if(countPauses!= int.Parse(messageValue[3]))
            {
                listOfActivities.DoPause();
            }
            
            x = float.Parse(messageValue[0], System.Globalization.CultureInfo.InvariantCulture);
            y = float.Parse(messageValue[1], System.Globalization.CultureInfo.InvariantCulture);
            countShots = int.Parse(messageValue[2]);
            countPauses = int.Parse(messageValue[3]);
            powerOfShot = int.Parse(messageValue[4]);

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
