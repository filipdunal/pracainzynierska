using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class calibrateGyro : MonoBehaviour
{
    [SerializeField] MeshRenderer target;
    SpriteRenderer srend;
    private void Awake()
    {
        srend = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log("AAAAAAAAAA");
    }
}
