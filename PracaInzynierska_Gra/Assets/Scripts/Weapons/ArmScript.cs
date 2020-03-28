using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ArmScript : MonoBehaviour
{
    public float maxFocusDistance=20f;
    WeaponSwitching weaponSwitching;
    [HideInInspector] public Transform targetObject;
    [HideInInspector] public Vector3 targetPoint;
    Player playerScript;
    Camera cam;
    float hitDistance = 5f;

    PostProcessVolume postProcessVolume;
    DepthOfField depthOfField;
    private void Start()
    {
        weaponSwitching = GetComponent<WeaponSwitching>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.Find("Camera").GetComponent<Camera>();


        postProcessVolume = GameObject.Find("Post Processing Volume").GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out depthOfField);
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
                hitDistance = Vector3.Distance(transform.position, hit.point);
            }
            else
            {
                targetObject = null;
            }
        }
        transform.LookAt(targetPoint);
        SetFocus();
        //Debug.Log(depthOfField.focusDistance.value);

    }

    void SetFocus()
    {
        if(hitDistance > maxFocusDistance)
        {
            depthOfField.focusDistance.value = maxFocusDistance;
        }
        else
        {
            depthOfField.focusDistance.value = hitDistance;
        }
    }
}
