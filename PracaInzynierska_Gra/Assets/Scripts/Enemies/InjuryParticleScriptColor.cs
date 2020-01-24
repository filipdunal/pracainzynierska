using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryParticleScriptColor : MonoBehaviour
{
    public Color fullHealth;
    public Color normalInjury;
    public Color criticalInjury;
    public Color dead;

    void SetColor(float health)
    {
        Color color;
        if(health>0f)
        {
            if(health>0.5f)
            {
                color = Color.Lerp(normalInjury, fullHealth, (float)(health - 0.5) * 2);
            }
            else
            {
                color=Color.Lerp(criticalInjury, normalInjury, (float)(health * 2));
            }
        }
        else
        {
            color = dead;
        }
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        ma.startColor = color;

    }
}
