using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    WeaponSwitching weaponSwitching;
    [HideInInspector] public Transform targetObject;
    [HideInInspector] public Vector3 targetPoint;
    Player playerScript;
    Camera cam;
    private void Start()
    {
        weaponSwitching = GetComponent<WeaponSwitching>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.Find("Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (playerScript.activeAimingAndShooting)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(CustomInputModule.mousePos);
            targetPoint = ray.origin + ray.direction * 100f;
            if (Physics.Raycast(ray, out hit, 100))
            {
                targetObject = hit.transform;
            }
            else
            {
                targetObject = null;
            }
        }
        transform.LookAt(targetPoint);
        //transform.rotation *= offsetRotation;
    }
}
