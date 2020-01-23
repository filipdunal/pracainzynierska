using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOfUser: MonoBehaviour
{
    public static SettingsOfUser Instance;
    
    public static string nick="*Player";
    public static int lastUsedNetworkPort = 25000;
    public static bool firstTime=true;
    public static int unlockedChapter = 1;

    private void Awake()
    {
        if(Instance==null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(gameObject);
        }
    }
}
