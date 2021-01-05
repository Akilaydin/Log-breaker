using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



[System.Serializable]
public class DataSave
{
    public int gameScore;
    public int appleScore;
}
public class Database : MonoBehaviour
{
    public static Database instance;
    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        DataSave data = new DataSave();
        bf.Serialize(file, data);
        file.Close();
    }

    public int LoadGameScore()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            DataSave data = (DataSave)bf.Deserialize(file);
            file.Close();
            return data.gameScore;
        }
        else
            Debug.LogError("There is no save data!");
        return 0;

    }

    public int LoadAppleScore()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            DataSave data = (DataSave)bf.Deserialize(file);
            file.Close();
            return data.appleScore;
        }
        else
            Debug.LogError("There is no save data!");
        return 0;

    }
}
