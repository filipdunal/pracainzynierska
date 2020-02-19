using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public Transform arena;
    public List<GameObject> monsterPrefabs;

    public void SpawnMonster(int monsterID)
    {
        //int spawnNumber = Random.Range(0, spawnPoints.Count);
        //Vector3 positionForSpawn = spawnPoints[spawnNumber].transform.position;
        //GameObject enemy = Instantiate(monsterPrefabs[monsterID],positionForSpawn,Quaternion.identity,gameObject.transform) as GameObject;
    }

    public void SpawnMonster(GameObject monsterPrefab)
    {
        //int spawnNumber = Random.Range(0, spawnPoints.Count);
        //Vector3 positionForSpawn = spawnPoints[spawnNumber].transform.position;
        //GameObject enemy = Instantiate(monsterPrefab, positionForSpawn, Quaternion.identity, gameObject.transform) as GameObject;
    }
    

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }
}
