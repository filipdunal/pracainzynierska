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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        destination = player.position;
        transform.position = new Vector3(transform.position.x, flightAltitude, transform.position.z);
        destination.y = flightAltitude;
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, destination);
        if (distance < noseDiveStartDistance)
        {
            NoseDive();
        }
        transform.LookAt(destination);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    void NoseDive()
    {
        destination = Vector3.Lerp(destination, player.position, Time.deltaTime * noseDivingSpeed);
    }
}
