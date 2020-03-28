using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLighter : MonoBehaviour
{
    public float speedOfFlying = 10f;
    public float distanceMargin = 0.5f;
    Transform desire;
    public GameObject lighter;
    public GameObject spotlight;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            desire = lighter.transform;
            spotlight.SetActive(false);
        }
    }

    private void Update()
    {
        if(desire!=null)
        {
            transform.position = Vector3.Lerp(transform.position, desire.position, speedOfFlying * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, desire.rotation, speedOfFlying * Time.deltaTime);
            if(Vector3.Distance(transform.position,desire.position)<distanceMargin)
            {
                lighter.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
