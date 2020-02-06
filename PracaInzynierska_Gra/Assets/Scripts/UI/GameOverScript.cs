using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    Player playerScript;
    GameObject deadPanel;
    GameObject injuredPanel;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        deadPanel = transform.GetChild(0).gameObject;
        injuredPanel = transform.GetChild(1).gameObject;
    }

    public void Dead()
    {
       playerScript.activeAimingAndShooting = false;
       playerScript.gameOver = true;
       GetComponent<Canvas>().transform.GetChild(0).GetComponent<Animator>().Play("deadScreen");
    }

    public void Injured()
    {
        GetComponent<Canvas>().transform.GetChild(1).GetComponent<Animator>().Play("injuryFlash", -1, 0f);
    }
}
