using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadData : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        StartCoroutine(PauseAndGo());
    }
    IEnumerator PauseAndGo()
    {
        yield return new WaitForSecondsRealtime(3f);
        
        FindObjectOfType<SettingsOfUser>().LoadData();
        SceneManager.LoadScene("mainMenu");

    }
}
