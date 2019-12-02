using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroControl : MonoBehaviour
{
    bool gyroEnabled;
    Gyroscope gyro;

    public GameObject cameraObject;
    Quaternion rot;
    float offset=0f;

    public Text debugText;
    

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        cameraObject.transform.position = transform.position;
        gyroEnabled = EnableGyro();
        
    }

    bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    private void Update()
    {
        if(gyroEnabled)
        {
            cameraObject.transform.localRotation = gyro.attitude * rot;
        }
        
    }

    public void CalibrateGyro()
    {
        offset = cameraObject.transform.localRotation.eulerAngles.z;
        transform.rotation = Quaternion.Euler(90f, offset, 0f);
    }
}
