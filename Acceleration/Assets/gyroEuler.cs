using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gyroEuler : MonoBehaviour
{
    public Text x;
    public Text y;
    public Text z;
    Quaternion gyroData;
    Vector3 gyroDataEuler;
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        gyroData=Input.gyro.attitude;
        x.text = System.Math.Round(gyroData.eulerAngles.x, 1).ToString();
        y.text = System.Math.Round(gyroData.eulerAngles.y, 1).ToString();
        z.text = System.Math.Round(gyroData.eulerAngles.z, 1).ToString();
    }
}
