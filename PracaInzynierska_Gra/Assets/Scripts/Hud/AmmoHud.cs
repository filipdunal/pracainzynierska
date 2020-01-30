using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHud : MonoBehaviour
{
    [Header("Resources")]
    public Sprite bigDivided2Circle;
    public Sprite bigDivided8Circle;
    public Sprite bigDivided16Circle;
    public Sprite bigDivided32Circle;

    [Header("Image components")]
    public Image backgroundClips;
    public Image fillerClips;
    public Image backgroundAmmo;
    public Image fillerAmmo;

    [Header("Colors for every type of weapon")]
    public Color colorForPistol;
    public Color colorForAK;
    public Color colorForShotgun;
    
    public void AmmoHUDupdate(int category, int ammo, int maxAmmo, int clips)
    {
    }

    /*  Type 
     *  1 - pistol
     *  2 - AK
     *  3 - shotgun
     */
    public void RefreshImage(int maxAmmo, int type)
    {
        switch (maxAmmo)
        {
            case 2:
                backgroundAmmo.sprite = bigDivided2Circle;
                break;
            case 8:
                backgroundAmmo.sprite = bigDivided8Circle;
                break;
            case 16:
                backgroundAmmo.sprite = bigDivided8Circle;
                break;
            case 32:
                backgroundAmmo.sprite = bigDivided32Circle;
                break;
            default:
                backgroundAmmo.sprite = null;
                break;
        }
        fillerAmmo.sprite = backgroundAmmo.sprite;
        
        switch(type)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
}
