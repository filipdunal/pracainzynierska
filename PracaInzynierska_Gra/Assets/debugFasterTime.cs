using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugFasterTime : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
