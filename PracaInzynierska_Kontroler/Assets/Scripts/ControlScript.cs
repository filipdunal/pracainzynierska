using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    int actionClicked=0;
    Quaternion gyroAtt;
    Vector3 gyroAttEuler;

    //Euler values of gyro
    float x;
    float y;
    float z;

    //Rounded and parsed do string values of gyro
    string xs;
    string ys;
    string zs;

    string message;

    public void ActionClick()
    {
        actionClicked++;
    }
    private void Start()
    {
        //QualitySettings.vSyncCount = 0;    //Turn off V Sync to unlock frames
        //Application.targetFrameRate = 600; //To send as many messages as possible
        Input.gyro.enabled = true;
    }

    public void Update()
    {

        gyroAtt = Input.gyro.attitude;
        gyroAttEuler = gyroAtt.eulerAngles;
        x = gyroAttEuler.x;
        y = gyroAttEuler.y;
        z = gyroAttEuler.z;
       
        message = (int)x + "|" + (int)y + "|" + (int)z + "|" + actionClicked;
        NetworkClientUI.SendToPC(message);
    }
}
