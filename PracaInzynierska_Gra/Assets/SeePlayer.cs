using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RootMotionMonsterScript rootMotionMonsterScript = GetComponentInParent<RootMotionMonsterScript>();
            if(rootMotionMonsterScript!=null)
            {
                rootMotionMonsterScript.triggered = true;
            }
            else
            {
                FlyerScript flyerScript = GetComponentInParent<FlyerScript>();
                if(flyerScript!=null)
                {
                    flyerScript.triggered = true;
                }
            }
            Destroy(gameObject);
        }
    }
}
