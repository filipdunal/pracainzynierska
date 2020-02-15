using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySelectedChapter : MonoBehaviour
{
    public void PlayChapter()
    {
        SettingsOfUser settings = FindObjectOfType<SettingsOfUser>();
        FindObjectOfType<SettingsOfUser>().GoToChapter(settings.chapterToLaunch);
    }
}
