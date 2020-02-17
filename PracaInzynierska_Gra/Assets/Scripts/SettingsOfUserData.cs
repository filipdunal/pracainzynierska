using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsOfUserData
{
    public string nick;
    public int lastUsedNetworkPort;
    public Weapon[] weapons;
    public Chapter[] chapters;
    public SettingsOfUserData(SettingsOfUser settingsOfUser)
    {
        nick = SettingsOfUser.nick;
        lastUsedNetworkPort = SettingsOfUser.lastUsedNetworkPort;
        weapons = settingsOfUser.weapons;
        chapters = settingsOfUser.chapters;
    }

}
