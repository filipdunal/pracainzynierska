using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int adrenaline;
    public bool activeAimingAndShooting;

    [HideInInspector] public int healthMax;
    [HideInInspector] public Transform targetObject;
    [HideInInspector] public Vector3 targetPoint;
    GameOverScript gameOverScript;

    Camera cam;

    private void Start()
    {
        //cam = transform.GetChild(0).GetComponent<Camera>();
        healthMax = health;
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        gameOverScript = GameObject.Find("Injury Canvas").GetComponent<GameOverScript>();
    }
    public void TakeDamage(int strength)
    {
        health -= strength;
        gameOverScript.Injured();
        if(health<=0)
        {
            gameOverScript.Dead();
        }

    }

    public void Shot()
    {
        if (targetObject!=null && targetObject.tag == "Monster")
        {
            targetObject.GetComponent<MonsterScript>().TakeDamage(30);
        }
    }

    private void Update()
    {
        if(activeAimingAndShooting)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(CustomInputModule.mousePos);
            targetPoint = ray.origin + ray.direction * 100f;
            if (Physics.Raycast(ray, out hit,100))
            {
                targetObject = hit.transform;
            }
            else
            {
                targetObject = null;
            }
        }
    }

    public void SwitchActiveAimingAndShooting(bool condition)
    {
        activeAimingAndShooting = condition;
    }
}
