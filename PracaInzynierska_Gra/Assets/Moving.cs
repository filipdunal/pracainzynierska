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
        lookingAt = transform.rotation;
        movementSpeed = walkSpeed;
    }

    public void NextWaypoint(bool run)
    {
        if(wayPoints.Capacity>(currentWaypointId+1))
        {
            currentWaypointId++;
            currentWaypoint = wayPoints[currentWaypointId];
            if(run)
            {
                movementSpeed = runSpeed;
            }
            else
            {
                movementSpeed = walkSpeed;
            }
        }
        else
        {
            movingCompleted = true;
            GetComponent<Player>().reachedFinalWaypoint = true;
            
        }
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
