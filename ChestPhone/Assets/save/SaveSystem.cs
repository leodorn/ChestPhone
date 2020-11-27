using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void savePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath,"player.save");
        FileStream stream = new FileStream(path, FileMode.Create);

        try
        {
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        finally
        {
            stream.Close();
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.save");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
            finally
            {
                stream.Close();
            }
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
