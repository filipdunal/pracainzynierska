using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScript : MonoBehaviour
{
    Player playerScript;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Dead()
    {
        playerScript.gameOver = true;
        GetComponent<Animator>().Play("deadScreen");
    }
}
