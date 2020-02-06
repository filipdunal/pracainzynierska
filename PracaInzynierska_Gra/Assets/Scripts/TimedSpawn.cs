using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawn : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int timeDelayFromStart;
    public bool spawned;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeDelayFromStart);
        GetComponent<Spawning>().SpawnMonster(monsterPrefab);
        spawned = true;
    }
    
}
