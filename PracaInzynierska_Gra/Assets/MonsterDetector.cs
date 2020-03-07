using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetector : MonoBehaviour
{
    public bool thereAreMonsters;
    List<Collider> triggerList;
    private void Start()
    {
        triggerList = new List<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Monster" && !triggerList.Contains(other))
        {
            triggerList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster" && triggerList.Contains(other))
        {
            triggerList.Remove(other);
        }
    }

    private void Update()
    {
        thereAreMonsters = (triggerList.Count != 0);
    }


}
