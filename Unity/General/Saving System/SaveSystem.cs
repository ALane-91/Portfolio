using System.Collections;
using System.Collections.Generic;
//Needs both the System.IO and the Systems.Runtime lines to enable use of BinaryFormatters.
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Static is required to prevent multiple instances of the SaveSystem being initiated.
public static class SaveSystem
{
    public static void SavePlayer(CharacterController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //The file can have any format, .doc, .dance, .fire, anything.
        string path = Application.persistentDataPath + "/player.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        //When called will save using the data from SaveData.cs.
        SaveData data = new SaveData(player);

        formatter.Serialize(stream, data);
    }

    public static SaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("The save file doesn't exist in specified location");
            return null;
        }
    }

}
