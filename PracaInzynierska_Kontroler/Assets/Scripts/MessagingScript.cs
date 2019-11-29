using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagingScript : MonoBehaviour
{
    public GameObject cameraRay;
    string message;
    float x;
    float y;

    private void Start()
    {
        message = "";
    }
    private void Update()
    {
        x = cameraRay.GetComponent<CameraRay>().cords[0];
        y = cameraRay.GetComponent<CameraRay>().cords[1];

        message = x + "|" + y;

        NetworkClientUI.SendToPC(message);
    }
}
