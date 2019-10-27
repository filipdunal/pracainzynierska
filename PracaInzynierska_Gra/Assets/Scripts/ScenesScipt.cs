using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesScipt : MonoBehaviour
{
    public void SwitchToScene(string name)
    {
        //Async loading goes here in future
        SceneManager.LoadScene(name);
    }
}
