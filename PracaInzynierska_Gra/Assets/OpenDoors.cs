using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public float desiredRotation;
    public float speedOfRotation;
    public bool start;
    Quaternion rot;
    private void Start()
    {
        rot = Quaternion.Euler(new Vector3(0f, desiredRotation, 0f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            start = true;
        }
    }
    private void Update()
    {
        if(start)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, speedOfRotation * Time.deltaTime);
        }
    }
}
