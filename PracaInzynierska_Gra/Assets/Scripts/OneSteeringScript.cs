using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSteeringScript : MonoBehaviour
{
    public CanvasGroup pauseMenu;
    Player player;
    WeaponSwitching weaponSwitching;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weaponSwitching = GameObject.Find("Arm / Weapon holder").GetComponent<WeaponSwitching>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            player.activeAimingAndShooting = !player.activeAimingAndShooting;
            if(Time.timeScale!=0f)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            DoPause();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            ShotManually();
        }
        if(Input.GetMouseButtonDown(0))
        {
            weaponSwitching.Shot();
        }
    }
    public void DoPause()
    {
        if(pauseMenu!=null)
        {
            if (pauseMenu.interactable)
            {
                pauseMenu.interactable = false;
                pauseMenu.alpha = 0;
                pauseMenu.blocksRaycasts = false;
            }
            else
            {
                pauseMenu.interactable = true;
                pauseMenu.alpha = 1;
                pauseMenu.blocksRaycasts = true;
            }
        }
    }
    public void ShotManually()
    {
        GetComponent<CustomInputModule>().SendMouseLeftClick();
    }
    public void ReleaseShotManually()
    {
        GetComponent<CustomInputModule>().ReleaseMouseLeftClick();
    }
}
