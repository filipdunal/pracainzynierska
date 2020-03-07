using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstPersonCameraScript : MonoBehaviour
{
    public float rotationOffsetStrength;
    public float smoothRate;
    public float speedOfChangingFOV = 10f;

    Vector3 rotationOffset;
    Quaternion originalRotation;
    Quaternion rotationToSet;

    float offsetHorizontal;
    float offsetVertical;

    Player player;
    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin perlin;
    float targetFOV = 40f;


    private void Start()
    {
        rotationOffset = Vector3.zero;
        originalRotation = transform.localRotation;

        player =GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        vcam = GetComponent<CinemachineVirtualCamera>();
        perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

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
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotationToSet, Time.deltaTime * smoothRate);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView,targetFOV,Time.deltaTime*speedOfChangingFOV);

    }

    public void SetAdrenaline(float value)
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = value;
    }

    public void SetFOV(float fov)
    {
        targetFOV= fov;
    }

    public void SetNoiseAmplitude(float noise)
    {
        perlin.m_AmplitudeGain = noise;
    }
}
