using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesScipt : MonoBehaviour
{
    void SwitchToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
