using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterScript : MonoBehaviour
{
    Transform desiredTransform;
    Quaternion originalRotation;
    //Vector3 originalPosition;
    private void Start()
    {
        desiredTransform = transform.parent.GetChild(0);
        originalRotation = transform.localRotation;
        //originalPosition = transform.localPosition;
        
    }
    private void Update()
    {
        transform.localRotation = desiredTransform.localRotation*originalRotation;
        //transform.localPosition = desiredTransform.localPosition + originalPosition;
        //transform.position = originalPosition + desiredTransform.position;
    }
}
