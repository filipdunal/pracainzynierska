using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextWaypoint : MonoBehaviour
{
    public bool run;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            other.GetComponent<Moving>().NextWaypoint(run);
        }
    }
}
