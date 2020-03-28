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
    AudioSource audioSource;

    public bool triggered;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        animator.speed = 0f;
    }
    private void Update()
    {
        if(triggered)
        {
            destination = player.position;
            destination.y = transform.position.y;
            transform.LookAt(destination);
            if (speedOfRootMotion > 0f)
            {
                animator.speed = speedOfRootMotion;
            }
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        
    }
}
