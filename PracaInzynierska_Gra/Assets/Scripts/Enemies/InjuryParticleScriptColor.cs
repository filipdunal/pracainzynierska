using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryParticleScriptColor : MonoBehaviour
{
    void SetColor(float health)
    {
        //Debug.Log(health);
        Color color;
        if(health>0f)
        {
            color = Color.Lerp(Color.red, Color.yellow, health);
        }
        else
        {
            color = Color.blue;
        }
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        ma.startColor = color;

    }
}
