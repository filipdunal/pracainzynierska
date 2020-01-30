using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudInfo : MonoBehaviour
{
    Image healthCircle;
    Player player;
    public float speedOfChaning;

    public Color fullHealth;
    public Color normalInjury;
    public Color criticalInjury;

    float health;
    float healthMax;
    float newHealth;

    float healthPercent = 1f;

    private void Start()
    {
        healthCircle = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() ;
    }

    private void Update()
    {
        healthMax = player.healthMax;
        newHealth = player.health;
        if(Mathf.Abs(health-newHealth)>0.001f)
        {
            health = Mathf.Lerp(health, newHealth, Time.unscaledDeltaTime * speedOfChaning);
            healthPercent = (float)health / (float)healthMax;
            healthCircle.fillAmount = healthPercent;
            ChangeColor();
        }
        
        
    }

    void ChangeColor()
    {
        Color color;
        if (healthPercent > 0.5f)
        {
            color = Color.Lerp(normalInjury, fullHealth, (float)(healthPercent - 0.5) * 2);
        }
        else
        {
            color = Color.Lerp(criticalInjury, normalInjury, (float)(healthPercent * 2));
        }
        healthCircle.color = color;


    }
}
