using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject monsterPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            SpawnNow();
        }
    }

    void SpawnNow()
    {
        Transform enemiesFolder = GameObject.Find("Enemies").transform;
        GameObject monster = Instantiate(monsterPrefab, transform.parent.position, Quaternion.identity, enemiesFolder);
        monster.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

    }
}
