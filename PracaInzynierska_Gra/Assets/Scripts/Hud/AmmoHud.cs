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
                fillerAmmo.color = colorForPistol;
                break;
            case 2:
                fillerAmmo.color = colorForAK;
                break;
            case 3:
                fillerAmmo.color = colorForShotgun;
                break;
            default:
                fillerAmmo.color = Color.white;
                break;
        }
        fillerClips.color = fillerAmmo.color;
    }

    public void AmmoUpdate(int ammoCurrent, int maxAmmo, int clipsCurrent)
    {
        fillerAmmo.fillAmount = (float)ammoCurrent / (float)maxAmmo;
        fillerClips.fillAmount = (float)clipsCurrent / 16f;
    }
}
