using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    public bool actionClicked;
    char actionClickedChar;
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

    public void actionClick()
    {
        actionClicked = true;
    }
    private void Start()
    {
        QualitySettings.vSyncCount = 0;    //Turn off V Sync to unlock frames
        Application.targetFrameRate = 600; //To send as many messages as possible
        Input.gyro.enabled = true;
    }

    public void Update()
    {

        gyroAtt = Input.gyro.attitude;
        gyroAttEuler = gyroAtt.eulerAngles;
        x = gyroAttEuler.x;
        y = gyroAttEuler.y;
        z = gyroAttEuler.z;

        xs = System.Math.Round(x, 3).ToString();
        ys = System.Math.Round(y, 3).ToString();
        zs = System.Math.Round(z, 3).ToString();

        if(actionClicked)   actionClickedChar = '1';
        else                actionClickedChar = '0';

        message = xs + "|" + ys + "|" + zs + "|" + actionClickedChar;
        NetworkClientUI.SendToPC(message);
        actionClicked = false;
    }
}
