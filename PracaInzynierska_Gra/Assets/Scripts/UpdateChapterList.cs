using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateChapterList : MonoBehaviour
{
    GridLayoutGroup gridLayoutGroup;
    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        StartCoroutine(SkipFirstFrame());
        
    }

    //First frame needs to be skiped because SettingsOfUser script which stores info about chapters has outdated data in the before Awake method.
    IEnumerator SkipFirstFrame()
    {
        yield return 0;
        SettingsOfUser settingsOfUser = FindObjectOfType<SettingsOfUser>();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Chapter ch in settingsOfUser.chapters)
        {
            foreach (Transform child in transform)
            {
                if ((child.gameObject.activeSelf || (ch.fileName == child.name && ch.unlocked)) && ch.passed)
                {
                    child.GetComponentInChildren<CanvasGroup>().alpha = 1f;
                }
                child.gameObject.SetActive(child.gameObject.activeSelf || (ch.fileName == child.name && ch.unlocked));

            }
        }
    }
    private void Update()
    {
        gridLayoutGroup.padding.left = (int)gridLayoutGroup.spacing.x;
    }
}
