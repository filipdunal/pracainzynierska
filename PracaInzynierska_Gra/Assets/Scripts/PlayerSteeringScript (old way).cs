using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerSteeringScript : MonoBehaviour
{
    string[] messageValue;

    //Received values
    float x=0f;
    float y=0f;
    int numberOfShots=0;
    int powerOfShot=0;

    public Text numShots;
    public GameObject celownik;
    RectTransform rtCelownik;
    public GameObject cursorCanvas;
    RectTransform rtCursorCanvas;
    public float lerpStrength = 0.5f;


    private void Start()
    {
        rtCelownik = celownik.GetComponent<RectTransform>();
        rtCursorCanvas = cursorCanvas.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Get values and assign them
        if(NetworkServerUI.receivedString!=null)
        {
            Debug.Log("Message received");
            messageValue = NetworkServerUI.receivedString.Split('|');
            //0 - x
            //1 - y
            //2 - number of shots
            //3 - power of shot

            if(numberOfShots!= int.Parse(messageValue[2]))
            {
                numShots.text = "Nr: " + numberOfShots + " Power: " + int.Parse(messageValue[3]);
            }

            x = float.Parse(messageValue[0],System.Globalization.CultureInfo.InvariantCulture);
            y = float.Parse(messageValue[1], System.Globalization.CultureInfo.InvariantCulture);
            numberOfShots = int.Parse(messageValue[2]);
            powerOfShot = int.Parse(messageValue[3]);
            Aim();
        }
    }

    void Aim()
    {
        Vector2 v = new Vector2();
        
        v.x = (rtCursorCanvas.rect.width / 2) *x/100;
        v.y = (rtCursorCanvas.rect.height / 2) * y / 100;

        rtCelownik.anchoredPosition=Vector2.Lerp(rtCelownik.anchoredPosition,v,lerpStrength);
        

    }
}
