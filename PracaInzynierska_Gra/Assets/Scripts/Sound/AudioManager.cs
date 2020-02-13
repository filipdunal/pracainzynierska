using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach(Sound so in sounds)
        {
            so.source = gameObject.AddComponent<AudioSource>();
            so.source.clip = so.clip;
            so.source.volume = so.volume;
            so.source.pitch = so.pitch;
            so.source.loop = so.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
