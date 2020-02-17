using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SaveData(SettingsOfUser settingsOfUser)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath+"/savedata.staycalm";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsOfUserData data = new SettingsOfUserData(settingsOfUser);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsOfUserData LoadData()
    {
        string path = Application.persistentDataPath + "/savedata.staycalm";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            SettingsOfUserData data =  bf.Deserialize(stream) as SettingsOfUserData;
            stream.Close();
            return data;
            // ADD TRY CATCH HERE

        }
        else
        {
            Debug.LogWarning("Save data not found");
            return null;
        }
    }
}