﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    Player playerScript;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Win()
    {
        playerScript.gameOver = true;
        //playerScript.gameWon = true;
        GetComponent<Animator>().Play("winScreen");

        //DO SAVING GAME HERE
    }
}
