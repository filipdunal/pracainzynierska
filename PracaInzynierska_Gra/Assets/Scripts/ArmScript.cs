using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    Player playerScript;
    //Quaternion offsetRotation;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //offsetRotation = transform.localRotation;
    }
    private void Update()
    {
        transform.LookAt(playerScript.targetPoint);
        //transform.rotation *= offsetRotation;
    }
}
