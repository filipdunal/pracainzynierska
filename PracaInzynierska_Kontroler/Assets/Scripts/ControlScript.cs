using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text textX;
    public Text textY;
    public Text textZ;

    private void OnGUI()
    {
        textX.text = x.ToString();
        textY.text = y.ToString();
        textZ.text = z.ToString();

    }

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
        /*
        gyroAtt = Input.gyro.attitude;
        gyroAttEuler = gyroAtt.eulerAngles;
        
        x = gyroAttEuler.x;
        y = gyroAttEuler.y;
        z = gyroAttEuler.z;
        */
        gyroAtt = Input.gyro.attitude;
        x = gyroAtt.x;
        y = gyroAtt.y;
        z = gyroAtt.z;


        message = (int)x + "|" + (int)y + "|" + (int)z + "|" + actionClicked;
        NetworkClientUI.SendToPC(message);
    }
}
