using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveUpgrades (UpgradeLoader data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/UpgradeData.txt"; //%AppData%/LocalLow/Robin/Survivors
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData pData = new PlayerData(data);

        formatter.Serialize(stream, pData);
        stream.Close();
    }

    public static PlayerData LoadUpgrades()
    {
        string path = Application.persistentDataPath + "/UpgradeData.txt";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

}
