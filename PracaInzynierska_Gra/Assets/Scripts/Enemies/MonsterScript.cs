using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public float speed;
    public int attackStrength;
    public int health;
    int maxHealth;

    public bool isFlying;
    [Header("Matters only on flying monster")]
    public float flightAltitude = 2f;
    public float noseDiveStartDistance = 3f;
    public bool isNoseDiving;

    Transform player;
    Vector3 destination;

    private void Start()
    {
        maxHealth = health;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        destination = player.position;
        if (isFlying)
        {
            transform.position = new Vector3(transform.position.x, flightAltitude, transform.position.z);
            destination.y = flightAltitude;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            destination.y = 0f;
        }
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, destination);
        if (isFlying && distance< noseDiveStartDistance)
        {
            NoseDive();
        }
        transform.LookAt(destination);
        transform.Translate(Vector3.forward * Time.deltaTime*speed);
    }

    void NoseDive()
    {
        destination = Vector3.Lerp(destination, player.position, Time.deltaTime);
        if(!GetComponent<Animator>().GetBool("goToNoseDive"))
        {
            GetComponent<Animator>().SetBool("goToNoseDive", true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(attackStrength);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int strength)
    {
        health -= strength;
        DoParticle();
        if (health<=0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void DoParticle()
    {
        GameObject particle = Instantiate(GetComponentInParent<InjuryParticleScript>().particle, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        float healthInPercent = (float)health / (float)maxHealth;
        particle.SendMessage("SetColor", healthInPercent);
        Destroy(particle, 2f);
    }
}
