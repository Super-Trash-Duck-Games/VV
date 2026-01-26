using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SaveData(PersistentData pData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.VV";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(pData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/data.VV";
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
            return null;
        }
    }
}
