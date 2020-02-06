using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int adrenaline;
    public bool activeAimingAndShooting;
    public bool gameOver;

    [HideInInspector] public int healthMax;
    GameOverScript gameOverScript;
    
    private void Start()
    {
        healthMax = health;
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

    public void SwitchActiveAimingAndShooting(bool condition)
    {
        activeAimingAndShooting = condition;
    }
}
