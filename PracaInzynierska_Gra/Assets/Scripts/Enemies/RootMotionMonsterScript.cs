using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionMonsterScript : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float speedOfRootMotion = 1f;
    Transform player;
    Vector3 destination;
    Animator animator;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        destination = player.position;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        destination.y = 0f;
        transform.LookAt(destination);
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(speedOfRootMotion>0f)
        {
            animator.speed = speedOfRootMotion;
        }
    }
}
