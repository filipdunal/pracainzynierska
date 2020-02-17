using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int adrenaline;
    public bool activeAimingAndShooting;

    public bool gameOver;
    public bool spawningCompleted;
    //public bool gameWon;

    [HideInInspector] public int healthMax;
    InjuryScript injuryScript;
    DeadScript deadScript;
    Transform EnemiesObject;
    WinScript winScript;
    bool winScriptLaunched;
    
    private void Start()
    {
        winScript = GameObject.Find("Win").GetComponent<WinScript>();
        EnemiesObject = GameObject.Find("Enemies").transform;
        healthMax = health;
        injuryScript = GameObject.Find("Injured").GetComponent<InjuryScript>();
        deadScript = GameObject.Find("Dead").GetComponent<DeadScript>();
    }
    public void TakeDamage(int strength)
    {
        health -= strength;
        injuryScript.Injured();
        if(health<=0)
        {
            deadScript.Dead();
        }

    }

    public void SwitchActiveAimingAndShooting(bool condition)
    {
        activeAimingAndShooting = condition;
    }

    private void Update()
    {
        if(!winScriptLaunched && health>0 && spawningCompleted && EnemiesObject.childCount==0)
        {
            winScriptLaunched = true;
            winScript.Win();
        }
    }

    public void SpawningCompleted()
    {
        spawningCompleted = true;
    }



}
