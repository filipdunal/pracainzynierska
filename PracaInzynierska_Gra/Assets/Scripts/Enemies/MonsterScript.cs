using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public float distanceTolerance;
    public float speed;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward*Time.deltaTime*speed);

        float distance = Vector3.Distance(transform.position, player.position);
        if(distance<distanceTolerance)
        {
            Destroy(gameObject);
        }

    }
}
