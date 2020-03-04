using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetector : MonoBehaviour
{
    public bool thereAreMonsters;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster")
        {
            thereAreMonsters = true;
        }
    }
    private void LateUpdate()
    {
        thereAreMonsters = false;
    }
}
