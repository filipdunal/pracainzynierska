using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(GetComponentInParent<DamageMonsterScript>().attackStrength);
            Destroy(transform.parent.gameObject);
        }
    }
}
