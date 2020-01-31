using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomEventSystemObjectController : MonoBehaviour
{
    [HideInInspector] public static CustomEventSystemObjectController instance;
    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!instance)
        {
            instance = this;
            instance.GetComponent<OneSteeringScript>().enabled = IsArena();
        }  
        //otherwise, if we do, kill this thing
        else
        {
            instance.GetComponent<OneSteeringScript>().enabled = IsArena();
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    bool IsArena()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        //To do: Change it to compare first word instead of first 5 letters
        string sceneCategory = sceneName.Substring(0, 5);
        if(sceneCategory=="Arena")
        {
            return true;
        }
        return false;
    }
}
