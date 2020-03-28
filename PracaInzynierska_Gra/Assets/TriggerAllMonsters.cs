using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAllMonsters : MonoBehaviour
{
    public Transform enemiesFolder;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            TriggerMonsters();    
        }
    }

    void TriggerMonsters()
    {
        foreach(Transform child in enemiesFolder)
        {
            child.GetComponent<RootMotionMonsterScript>().triggered = true;
        }
    }
}
