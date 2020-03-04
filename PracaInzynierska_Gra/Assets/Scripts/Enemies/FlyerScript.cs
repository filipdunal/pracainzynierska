using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerScript : MonoBehaviour
{
    public float speed;
    public float flightAltitude = 2f;
    public float noseDiveStartDistance = 3f;
    public bool isNoseDiving;
    public float noseDivingSpeed = 1f;

    Transform player;
    Vector3 destination;

    public bool triggered;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if(triggered)
        {
            destination = player.position;
            destination.y = transform.position.y;

            float distance = Vector3.Distance(transform.position, destination);
            if (distance < noseDiveStartDistance)
            {
                NoseDive();
            }
            transform.LookAt(destination);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        
    }
    void NoseDive()
    {
        destination = Vector3.Lerp(destination, player.position, Time.deltaTime * noseDivingSpeed);
    }
}
