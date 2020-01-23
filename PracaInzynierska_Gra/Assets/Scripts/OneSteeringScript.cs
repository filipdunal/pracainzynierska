using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSteeringScript : MonoBehaviour
{
    public CanvasGroup pauseMenu;
    Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            player.activeAimingAndShooting = !player.activeAimingAndShooting;
            
            DoPause();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            Shot();
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
    public void Shot()
    {
        GetComponent<CustomInputModule>().SendMouseLeftclick();
    }
}
