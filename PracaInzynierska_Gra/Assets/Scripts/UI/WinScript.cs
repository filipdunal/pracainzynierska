using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public Text weaponUnlockedText;
    public Text chapterUnlockedText;

    Player playerScript;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Win()
    {
        playerScript.gameOver = true;
        GetComponent<Animator>().Play("winScreen");
        SettingsOfUser settingsOfUser = FindObjectOfType<SettingsOfUser>();
        settingsOfUser.SaveProgress(0);

        string weapon = settingsOfUser.GetNameOfUnclockedWeapon();
        string chapter = settingsOfUser.GetNameOfUnclockedChapter();

        if(weapon!="")
        {
            weaponUnlockedText.text = "Weapon unlocked: " + weapon;
        }
        else
        {
            weaponUnlockedText.text = "";
        }
        if(chapter!="")
        {
            chapterUnlockedText.text = "Chapter unlocked: " + chapter;
        }
        else
        {
            chapterUnlockedText.text = "";
        }



        //DO SAVING GAME HERE
    }
}
