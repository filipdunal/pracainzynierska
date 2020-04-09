using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Audio clips")]
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip outOfAmmoSound;

    public AudioSource audioSource;

    ArmScript armScript;
    ParticleSystem muzzleFlash;
    
    [Tooltip("1- pistol , 2- AK, 3- shotgun")]
    public int typeOfWeapon = 1;

    [Header("Ammo start values")]
    public int ammoClipCapacity=30;
    public int ammoClipsStartAmount=5;

    [Header("Ammo current values (only to visualize, don't change)")]
    public int ammoCurrentInClip;
    public int ammoClipsLeft;

    [Header("Bullet values")]
    public float fireRate = 15f;
    public int damageOfBullet = 30;
    public float range=30f;

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

    AmmoHud ammoHud;

    private void OnEnable() //To avoid looking for a component on every gun in the same time
    {
        if(ammoHud==null) //To look for component only once (when it's being chosen for the first time)
        {
            ammoHud = GameObject.Find("Ammo").GetComponent<AmmoHud>();
        }
        ammoHud.RefreshImage(ammoClipCapacity,typeOfWeapon);

        reloading = false; //To avoid bug of freezing weapon when it's changed during the reloading

    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(shotSound);
                
                if (armScript.targetObject != null && armScript.targetObject.tag == "Monster")
                {
                    Debug.Log(Vector3.Distance(transform.position, armScript.targetObject.position));
                    if(Vector3.Distance(transform.position,armScript.targetObject.position)<range)
                    {
                        armScript.targetObject.GetComponent<DamageMonsterScript>().TakeDamage(damageOfBullet);
                    }
                }
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
                audioSource.PlayOneShot(reloadSound);
                reloading = true;
                yield return new WaitForSeconds(reloadTime);
                reloading = false;
                ammoClipsLeft--;
                ammoCurrentInClip = ammoClipCapacity;
            }
        }
        else
        {
            audioSource.PlayOneShot(outOfAmmoSound);
        }
    }

    public void StartReloading()
    {
        StartCoroutine(Reload());
    }
    

    private void Update()
    {
        ammoHud.AmmoUpdate(ammoCurrentInClip, ammoClipCapacity, ammoClipsLeft);

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
