using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsOfUser : MonoBehaviour
{
    public static SettingsOfUser Instance;

    public static string nick = "*Player";
    public static int lastUsedNetworkPort = 25000;
    //public static bool firstTime = true;
    //public static int unlockedChapter = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public string _chapterToLaunch;
    public string chapterToLaunch
    {
       get { return _chapterToLaunch; }
       set { _chapterToLaunch = value; }
    }
    
    public Weapon[] weapons;
    public Chapter[] chapters;
    public void GoToChapter(string title)
    {
        foreach(Chapter ch in chapters)
        {
            if(ch.fileName==title)
            {
                SceneManager.LoadSceneAsync(title);
                return;
            }
        }
        Debug.LogWarning("Chapter to load: " + title + " not found");
    }
    public void SaveProgress(int score)
    {
        Scene scene = SceneManager.GetActiveScene();
        foreach(Chapter ch in chapters)
        {
            Debug.Log(scene.name);
            if(scene.name==ch.fileName)
            {
                ch.passed = true;
                // ch.highestScore = ....
                UnlockNextChapter(ch.passingUnlocksLevel);
                UnlockNextWeapon(ch.passingUnlocksWeapon);
                SaveData();
                return;
            }
        }
        Debug.LogWarning("Title to save: " + scene.name + " not found");
    }

    void UnlockNextChapter(string title)
    {
        if(title!="")
        {
            foreach (Chapter ch in chapters)
            {
                if (title == ch.fileName)
                {
                    ch.unlocked = true;
                    return;
                }
            }
            Debug.LogWarning("Title to unlock: " + title + " not found");
        }
        else
        {
            Debug.LogWarning("Title to unlock is empty");
        }   
    }

    void UnlockNextWeapon(string title)
    {
        if(title!="")
        {
            foreach(Weapon w in weapons)
            {
                if(title == w.prefabName)
                {
                    w.unlocked = true;
                }
            }
        }
    }
    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }
    public void LoadData()
    {
        SettingsOfUserData data;
        if (SaveSystem.LoadData()!=null)
        {
            data = SaveSystem.LoadData();
            nick = data.nick;
            lastUsedNetworkPort = data.lastUsedNetworkPort;
            weapons = data.weapons;
            chapters = data.chapters;
        } 

    }














}
