using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePauseInTheEndScript : MonoBehaviour
{
    public void ForcePause()
    {
        GameObject.Find("Custom EventSystem").GetComponent<OneSteeringScript>().ForcePause();
    }
}
