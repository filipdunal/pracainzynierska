using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextWaypoint : MonoBehaviour
{
    public int speed;
    public bool holdUntilAreaIsFreeFromMonsters;
    MonsterDetector monsterDetector;


    Transform enemiesFolder;
    public float holdForSeconds = 0f;
    Moving moving;

    private void Start()
    {
        monsterDetector = GetComponentInChildren<MonsterDetector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            moving = other.GetComponent<Moving>();
            if(!holdUntilAreaIsFreeFromMonsters)
            {
                if (holdForSeconds == 0)
                {
                    moving.NextWaypoint();
                    moving.SetSpeed(speed);
                }
                else
                {
                    moving.SetSpeed(0);
                    StartCoroutine(Hold(holdForSeconds));
                }
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            if(holdUntilAreaIsFreeFromMonsters)
            {
                if(monsterDetector.thereAreMonsters)
                {
                    moving.SetSpeed(0);
                }
                else
                {
                    moving.SetSpeed(speed);
                    moving.NextWaypoint();
                }
                
            }
        }
    }

    IEnumerator Hold(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        moving.NextWaypoint();
        moving.SetSpeed(speed);
    }

}
