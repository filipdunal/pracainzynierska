using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMonsterScript : MonoBehaviour
{
    public Vector3 offsetOfParticlePlace;
    public int attackStrength;
    public int health;
    int maxHealth;

    [Header("Audio clips")]
    public List<AudioClip> hurtSounds;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxHealth = health;
    }
    public void TakeDamage(int strength)
    {
        health -= strength;
        DoParticle();
        audioSource.PlayOneShot(hurtSounds[(int)Random.Range(0f, hurtSounds.Capacity - 1)]);
        if (health <= 0)
        {
            Die();  
        }
        
    }
    public void Die()
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
