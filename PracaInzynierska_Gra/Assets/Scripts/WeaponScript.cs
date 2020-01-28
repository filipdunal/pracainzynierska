using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    ArmScript armScript;
    ParticleSystem muzzleFlash;
    public float fireRate = 15f;
    int damageOfBullet = 30;

    float nextTimeToFire = 0f;
    
    private void Start()
    {
        armScript = GetComponentInParent<ArmScript>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }
    public void Shot()
    {
        if(Time.time>=nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            muzzleFlash.Play();
            if (armScript.targetObject != null && armScript.targetObject.tag == "Monster")
            {
                armScript.targetObject.GetComponent<MonsterScript>().TakeDamage(damageOfBullet);
            }
        }
        
    }

    private void Update()
    {
        
    }
}
