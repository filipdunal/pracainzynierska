using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextWaypoint : MonoBehaviour
{
    public int speed;
    public bool holdUntilAreaIsFreeFromMonsters;
    MonsterDetector monsterDetector;

    Transform enemiesFolder;
    public int holdSpeed = 0;
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
            moving.NextWaypoint();
            if (!holdUntilAreaIsFreeFromMonsters)
            {
                moving.SetSpeed(speed);
            }
            else
            {
                if(monsterDetector.thereAreMonsters)
                {
                    moving.SetSpeed(holdSpeed);
                    StartCoroutine(Hold(0.5f));
                }
                else
                {
                    moving.SetSpeed(speed);
                }
            }

            ShowInfoOnEnter info = GetComponent<ShowInfoOnEnter>();
            if (info != null)
            {
                FindObjectOfType<InfoSystem>().ShowMessage(info.message, info.time, info.color);
            }

        }
    }

    IEnumerator Hold(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if(monsterDetector.thereAreMonsters)
        {
            StartCoroutine(Hold(0.5f));
        }
        else
        {
            moving.SetSpeed(speed);
        }
        
    }

}
