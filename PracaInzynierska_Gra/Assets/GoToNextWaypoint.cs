using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextWaypoint : MonoBehaviour
{
    public int speed;
    public float holdForSeconds = 0f;
    Moving moving;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            moving = other.GetComponent<Moving>();
            if(holdForSeconds==0)
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

    IEnumerator Hold(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        moving.NextWaypoint();
        moving.SetSpeed(speed);
        
    }
}
