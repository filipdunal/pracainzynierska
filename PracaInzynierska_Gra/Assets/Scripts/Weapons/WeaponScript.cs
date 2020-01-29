﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    ArmScript armScript;
    ParticleSystem muzzleFlash;

    [Header("Ammo start values")]
    public int ammoClipCapacity=30;
    public int ammoClipsStartAmount=5;

    [Header("Ammo current values (only to visualize, don't change)")]
    public int ammoCurrentInClip;
    public int ammoClipsLeft;

    [Header("Bullet values")]
    public float fireRate = 15f;
    public int damageOfBullet = 30;

    float nextTimeToFire = 0f;

    Vector3 defaultPosition;
    Vector3 recoilPosition;
    bool shooting;
    bool reloading;

    [Header("Recoil settings")]
    public float recoilDistance=0.5f;
    public float recoilForce=70f;
    public float recoilGoingBackForce=5f;
    public float recoilDelayToGoBack=0.1f;

    [Header("Reload animation settings")]
    public float degreesToRotate = -45f;
    public float speedOfRotatingGun = 5f;
    public float reloadTime = 1f;

    Quaternion defaultRotation;
    Quaternion reloadingRotation;

    
    
    private void Start()
    {
        defaultRotation = transform.localRotation;

        ammoCurrentInClip = ammoClipCapacity;
        ammoClipsLeft = ammoClipsStartAmount;

        defaultPosition = transform.localPosition;
        recoilPosition = defaultPosition + new Vector3(0f, 0f, -recoilDistance);

        armScript = GetComponentInParent<ArmScript>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }
    public void Shot()
    {
        if (Time.time >= nextTimeToFire)
        {
            if(ammoCurrentInClip>0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                muzzleFlash.Play();
                if (armScript.targetObject != null && armScript.targetObject.tag == "Monster")
                {
                    armScript.targetObject.GetComponent<MonsterScript>().TakeDamage(damageOfBullet);
                }

                //StopAllCoroutines();
                StopCoroutine(DoRecoil());
                StartCoroutine(DoRecoil());

                ammoCurrentInClip--;
            }
            else
            {
                StartReloading();
            }
            
        }


    }
    IEnumerator DoRecoil()
    {
        shooting = true;
        yield return new WaitForSeconds(recoilDelayToGoBack);
        shooting = false;
    }

    IEnumerator Reload()
    {
        if(ammoClipsLeft>0)
        {
            if(!reloading)
            {
                reloading = true;
                yield return new WaitForSeconds(reloadTime);
                reloading = false;
                ammoClipsLeft--;
                ammoCurrentInClip = ammoClipCapacity;
            }
        } 
    }

    public void StartReloading()
    {
        StartCoroutine(Reload());
    }
    private void Update()
    {
        recoilPosition = defaultPosition + new Vector3(0f, 0f, -recoilDistance);
        
        if (shooting)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, recoilPosition, Time.deltaTime*recoilForce);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime*recoilGoingBackForce);
        }

        reloadingRotation = defaultRotation * Quaternion.Euler(0f, degreesToRotate, 0f);
        if(reloading)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, reloadingRotation, Time.deltaTime * speedOfRotatingGun);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, defaultRotation, Time.deltaTime * speedOfRotatingGun);
        }
    }
}
