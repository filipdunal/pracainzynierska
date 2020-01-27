using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    WeaponSwitching weaponSwitching;
    //Quaternion offsetRotation;
    private void Start()
    {
        weaponSwitching = GetComponent<WeaponSwitching>();
        //offsetRotation = transform.localRotation;
    }
    private void Update()
    {
        transform.LookAt(weaponSwitching.targetPoint);
        //transform.rotation *= offsetRotation;
    }
}
