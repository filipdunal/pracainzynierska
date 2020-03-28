using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchIndoorAmbient : MonoBehaviour
{
    public AudioClip nature;
    public AudioClip indoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            AudioSource audioSource = other.transform.GetChild(0).GetComponent<AudioSource>();
            audioSource.clip = indoor;
            audioSource.Play();
            audioSource.volume = 0.2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            AudioSource audioSource = other.transform.GetChild(0).GetComponent<AudioSource>();
            audioSource.clip = nature;
            audioSource.Play();
            audioSource.volume = 1f;
        }
    }
}
