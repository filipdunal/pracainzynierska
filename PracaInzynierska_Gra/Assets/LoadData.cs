using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadData : MonoBehaviour
{
    AsyncOperation asyncLoad;
    public GameObject pressAnyKey;
    public GameObject loadingIcon;
    bool waitingForKey;
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        StartCoroutine(PauseAndGo());
    }
    IEnumerator PauseAndGo()
    {
        yield return new WaitForSecondsRealtime(5f);
        FindObjectOfType<SettingsOfUser>().LoadData();
        asyncLoad = SceneManager.LoadSceneAsync("mainMenu");
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress<0.9f)
        {
            yield return null;
        }

        loadingIcon.SetActive(false);
        pressAnyKey.SetActive(true);
        waitingForKey = true;

    }
    private void Update()
    {
        if(waitingForKey && Input.anyKey)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }
}
