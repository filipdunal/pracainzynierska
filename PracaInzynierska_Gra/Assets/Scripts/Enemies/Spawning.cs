using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public Transform arena;
    List<GameObject> spawnPoints;
    List<GameObject> pieces;
    public GameObject monsterPrefab;

    private void Start()
    {
        RefreshList();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonster();
        }
    }

    void RefreshList()
    {
        pieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("Piece"));
        pieces.Sort(SortByName);

        spawnPoints = new List<GameObject>();
        foreach(GameObject child in pieces)
        {
            spawnPoints.Add(child.transform.Find("Spawner left down").gameObject);
            spawnPoints.Add(child.transform.Find("Spawner right down").gameObject);
        }
    }

    void SpawnMonster()
    {
        int randomSpawnNumber = Random.Range(0, spawnPoints.Count);
        Vector3 positionForSpawn = spawnPoints[randomSpawnNumber].transform.position;
        GameObject enemy = Instantiate(monsterPrefab,positionForSpawn,Quaternion.identity) as GameObject;
    }

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }
}
