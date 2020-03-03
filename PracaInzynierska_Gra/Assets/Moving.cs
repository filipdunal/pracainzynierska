using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    float movementSpeed;
    public float walkSpeed = 1f;
    public float runSpeed = 2f;


    public float lookRotationLerp = 1f;
    public List<Transform> wayPoints;
    Transform currentWaypoint;
    int currentWaypointId;
    bool movingCompleted;
    Quaternion lookingAt;

    private void Start()
    {
        currentWaypointId = 0;
        currentWaypoint = wayPoints[0];

        UpdateColliders();
        currentWaypoint.gameObject.GetComponent<BoxCollider>().enabled = true;

        lookingAt = transform.rotation;
        movementSpeed = walkSpeed;
    }

    public void NextWaypoint()
    {
        if(wayPoints.Capacity>(currentWaypointId+1))
        {
            currentWaypointId++;
            currentWaypoint = wayPoints[currentWaypointId];
            UpdateColliders();
        }
        else
        {
            movingCompleted = true;
            GetComponent<Player>().reachedFinalWaypoint = true;
            
        }
    }

    public void SetSpeed(int speed)
    {
        switch(speed)
        {
            case 0:
                movementSpeed = 0;
                break;
            case 1:
                movementSpeed = walkSpeed;
                break;
            case 2:
                movementSpeed = runSpeed;
                break;
            default:
                break;
        }
    }

    void UpdateColliders()
    {
        foreach (Transform wp in wayPoints)
        {
            wp.GetComponent<BoxCollider>().enabled = false;
        }
        currentWaypoint.gameObject.GetComponent<BoxCollider>().enabled = true;

    }
    private void Update()
    {
        if(!movingCompleted)
        {
            lookingAt = Quaternion.Lerp(lookingAt, Quaternion.LookRotation(currentWaypoint.position - transform.position),Time.deltaTime*lookRotationLerp*(movementSpeed/walkSpeed));
            transform.rotation = lookingAt;
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }
}
