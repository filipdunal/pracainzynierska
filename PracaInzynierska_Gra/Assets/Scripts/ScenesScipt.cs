using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesScipt : MonoBehaviour
{
    public void SwitchToScene(string name)
    {
        //Async loading goes here in future
        RefreshSceneSettings();
        SceneManager.LoadScene(name);
    }

    public void RestartScene()
    {
        RefreshSceneSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RefreshSceneSettings()
    {
        Time.timeScale = 1f;
        GameObject.Find("Custom EventSystem").GetComponent<OneSteeringScript>().enabled = false;
    }
}
