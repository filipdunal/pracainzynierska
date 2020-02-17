using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chapter
{
    public string name;
    public string fileName;
    public bool unlocked;
    public bool passed;
    public int highestScore;

    //public string whatPassingUnlocks;
    public string passingUnlocksLevel;
    public string passingUnlocksWeapon;
}
