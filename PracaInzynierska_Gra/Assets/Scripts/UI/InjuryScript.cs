using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryScript : MonoBehaviour
{
    Player playerScript;
    //GameObject deadPanel;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //deadPanel = transform.GetChild(0).gameObject;
    }

    /*
    public void Dead()
    {
       playerScript.gameOver = true;
       GetComponent<Canvas>().transform.GetChild(0).GetComponent<Animator>().Play("deadScreen");
    }
    */

    public void Injured()
    {
        GetComponent<Animator>().Play("injuryFlash", -1, 0f);
    }
}
