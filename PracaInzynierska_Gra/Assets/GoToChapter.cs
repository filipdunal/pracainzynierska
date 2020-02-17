using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToChapter : MonoBehaviour
{
    // This class was created because it's not possible to attach component with DontDestroyOnLoad in the inspector in OnClickEvent
    public void GoTo(string chapter)
    {
        FindObjectOfType<SettingsOfUser>().chapterToLaunch = chapter;
    }
}
