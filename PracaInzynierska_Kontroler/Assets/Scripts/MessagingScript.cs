using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagingScript : MonoBehaviour
{
    public GameObject cameraRay;
    public GameObject shotSlider;

    Slider sl;
    string message;
    float x;
    float y;
    int powerOfShot = 0;
    int numberOfShots = 0;

    private void Start()
    {
        sl = shotSlider.GetComponent<Slider>();
        message = "";
    }
    private void Update()
    {
        x = cameraRay.GetComponent<CameraRay>().cords[0];
        y = cameraRay.GetComponent<CameraRay>().cords[1];
        powerOfShot = (int)sl.value;

        message = x + "|" + y + "|" + numberOfShots +  "|" +powerOfShot;
        NetworkClientUI.SendToPC(message);
    }

    public void Shot()
    {
        numberOfShots++;
    }
    
}
