using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public bool activeAimingAndShooting;
    GameOverScript gameOverScript;

    Camera cam;

    private void Start()
    {
        //cam = transform.GetChild(0).GetComponent<Camera>();
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
        if(activeAimingAndShooting)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(CustomInputModule.mousePos);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.tag == "Monster")
                {
                    objectHit.GetComponent<MonsterScript>().TakeDamage(100);
                }
            }
        }
    }

    public void SwitchActiveAimingAndShooting(bool condition)
    {
        activeAimingAndShooting = condition;
    }
}
