using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaySelectedChapter : MonoBehaviour
{
    public ControlOfSelectedWeapons controlOfSelectedWeapons;
    public Color noWeaponColor;
    [TextArea] public string noWeaponText;
    public float timeToWait = 1f;

    public Text text;
    public Image image;
    
    public void PlayChapter()
    {
        if(controlOfSelectedWeapons.TakeSelectedWeapons())
        {
            SettingsOfUser settings = FindObjectOfType<SettingsOfUser>();
            FindObjectOfType<SettingsOfUser>().GoToChapter(settings.chapterToLaunch);
        }
        else
        {
            ShowWarningOfNoWeapons();
        }
    }

    void ShowWarningOfNoWeapons()
    {
        string defaultString = text.text;
        Color defaultColor = image.color;

        text.text = noWeaponText;
        image.color = noWeaponColor;

        StopAllCoroutines();
        StartCoroutine(DisableWarning(defaultString,defaultColor));
    }

    IEnumerator DisableWarning(string defaultString, Color defaultColor)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        text.text = defaultString;
        image.color = defaultColor;

    }
}
