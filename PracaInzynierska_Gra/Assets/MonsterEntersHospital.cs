using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEntersHospital : MonoBehaviour
{
    public Player playerScript;
    public GameObject textMonstersEnteredHospital;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Monster")
        {
            EndGame();
        }
    }

    void EndGame()
    {
        playerScript.TakeDamage(playerScript.health);
        textMonstersEnteredHospital.SetActive(true);
    }
}
