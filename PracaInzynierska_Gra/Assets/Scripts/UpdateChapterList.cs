using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateChapterList : MonoBehaviour
{
    void Start()
    {
        SettingsOfUser settingsOfUser = FindObjectOfType<SettingsOfUser>();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Chapter ch in settingsOfUser.chapters)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(child.gameObject.activeSelf || (ch.fileName == child.name && ch.unlocked));
            }
        }
    }
}
