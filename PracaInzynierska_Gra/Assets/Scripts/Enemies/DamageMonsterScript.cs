using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMonsterScript : MonoBehaviour
{
    public Vector3 offsetOfParticlePlace;
    public int attackStrength;
    public int health;
    int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }
    public void TakeDamage(int strength)
    {
        health -= strength;
        DoParticle();
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        transform.position = new Vector3(0, 1000, 0);
        StartCoroutine(DestroyMeNextFrame());
    }
    IEnumerator DestroyMeNextFrame()
    {
        yield return 0;
        Destroy(gameObject);
    }

    void DoParticle()
    {
        GameObject particle = Instantiate(GetComponentInParent<InjuryParticleScript>().particle, transform.position + offsetOfParticlePlace, Quaternion.identity);
        float healthInPercent = (float)health / (float)maxHealth;
        particle.SendMessage("SetColor", healthInPercent);
        Destroy(particle, 2f);
    }
}
