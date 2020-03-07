using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    float movementSpeed;

    [Header("Moving speed")]
    public float slowWalkSpeed = 0.5f;
    public float walkSpeed = 1f;
    public float runSpeed = 2f;

    [Header("FOV while moving")]
    public float idleFOV = 40f;
    public float slowWalkFOV = 40f;
    public float walkFOV = 40f;
    public float runFOV = 40f;

    [Header("Noise while moving")]
    public float idleNoise = 0.2f;
    public float slowWalkNoise = 0.2f;
    public float walkNoise = 0.2f;
    public float runNoise = 0.2f;


    public float lookRotationLerp = 1f;
    public List<Transform> wayPoints;
    Transform currentWaypoint;
    int currentWaypointId;
    bool movingCompleted;
    Quaternion lookingAt;
    FirstPersonCameraScript fpsCamera;

    private void Start()
    {
        currentWaypointId = 0;
        currentWaypoint = wayPoints[0];

        UpdateColliders();
        currentWaypoint.gameObject.GetComponent<BoxCollider>().enabled = true;

        lookingAt = transform.rotation;
        movementSpeed = walkSpeed;

        fpsCamera = GetComponentInChildren<FirstPersonCameraScript>();
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
                fpsCamera.SetFOV(idleFOV);
                fpsCamera.SetNoiseAmplitude(idleNoise);
                break;
            case 1:
                movementSpeed = slowWalkSpeed;
                fpsCamera.SetFOV(slowWalkFOV);
                fpsCamera.SetNoiseAmplitude(slowWalkNoise);
                break;
            case 2:
                movementSpeed = walkSpeed;
                fpsCamera.SetFOV(walkFOV);
                fpsCamera.SetNoiseAmplitude(walkNoise);
                break;
            case 3:
                movementSpeed = runSpeed;
                fpsCamera.SetFOV(runFOV);
                fpsCamera.SetNoiseAmplitude(runNoise);
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
