using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraScript : MonoBehaviour
{
    public float rotationOffsetStrength;
    Vector3 rotationOffset;
    Quaternion originalRotation;

    float offsetHorizontal;
    float offsetVertical;

    Player player;
    

    private void Start()
    {
        rotationOffset = Vector3.zero;
        originalRotation = transform.rotation;

        player = GetComponentInParent<Player>();
    }
    private void Update()
    {
        if(player.activeAimingAndShooting)
        {
            offsetHorizontal = ((1f - (Mathf.Abs(CustomInputModule.mousePos.x - Screen.width) / Screen.width)) - 0.5f) * 2;
            offsetVertical = -((1f - (Mathf.Abs(CustomInputModule.mousePos.y - Screen.height) / Screen.height)) - 0.5f) * 2;

            rotationOffset = new Vector3(offsetVertical, offsetHorizontal, 0f);
            rotationOffset *= rotationOffsetStrength;

            transform.rotation = originalRotation * Quaternion.Euler(rotationOffset);
        }
        
    }
}
