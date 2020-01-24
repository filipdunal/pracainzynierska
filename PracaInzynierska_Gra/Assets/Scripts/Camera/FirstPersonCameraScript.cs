using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstPersonCameraScript : MonoBehaviour
{
    public float rotationOffsetStrength;
    public float smoothRate;

    Vector3 rotationOffset;
    Quaternion originalRotation;
    Quaternion rotationToSet;

    float offsetHorizontal;
    float offsetVertical;

    Player player;
    CinemachineVirtualCamera vcam;


    private void Start()
    {
        rotationOffset = Vector3.zero;
        originalRotation = transform.rotation;

        player =GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        vcam = GetComponent<CinemachineVirtualCamera>();

    }
    private void Update()
    {
        if(player.activeAimingAndShooting)
        {
            offsetHorizontal = ((1f - (Mathf.Abs(CustomInputModule.mousePos.x - Screen.width) / Screen.width)) - 0.5f) * 2;
            offsetVertical = -((1f - (Mathf.Abs(CustomInputModule.mousePos.y - Screen.height) / Screen.height)) - 0.5f) * 2;

            rotationOffset = new Vector3(offsetVertical, offsetHorizontal, 0f);
            rotationOffset *= rotationOffsetStrength;

            rotationToSet = originalRotation * Quaternion.Euler(rotationOffset);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationToSet, Time.deltaTime * smoothRate);
        
    }

    public void SetAdrenaline(float value)
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = value;
    }
}
