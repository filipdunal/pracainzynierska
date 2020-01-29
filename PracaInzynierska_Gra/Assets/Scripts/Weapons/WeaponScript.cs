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

    Vector3 defaultPosition;
    Vector3 recoilPosition;
    bool shooting;

    public float recoilDistance=0.5f;
    public float recoilForce=70f;
    public float recoilGoingBackForce=5f;
    public float recoilDelayToGoBack=0.1f;

    private void Start()
    {
        defaultPosition = transform.localPosition;
        recoilPosition = defaultPosition + new Vector3(0f, 0f, -recoilDistance);

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
            StopAllCoroutines();
            StartCoroutine(DoRecoil());
        }
        
    }
    IEnumerator DoRecoil()
    {
        shooting = true;
        yield return new WaitForSeconds(recoilDelayToGoBack);
        shooting = false;
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
    }
}
