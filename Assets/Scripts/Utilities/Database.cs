using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



[System.Serializable]
public class AppleScoreSave
{
    public int appleScore;
}
[System.Serializable]
public class GameScoreSave
{
    public int gameScore;
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


    public void SaveGameScore(int gameScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameScore.dat");
        GameScoreSave data = new GameScoreSave();
        data.gameScore = gameScore;
        bf.Serialize(file, data);
        file.Close();
    }
    public void SaveApplesScore(int appleScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ApplesData.dat");
        AppleScoreSave data = new AppleScoreSave();
        data.appleScore = appleScore;
        bf.Serialize(file, data);
        file.Close();
    }
    public int LoadGameScore()
    {
        if (File.Exists(Application.persistentDataPath + "/GameScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameScore.dat", FileMode.Open);
            GameScoreSave data = (GameScoreSave)bf.Deserialize(file);
            file.Close();
            return data.gameScore;
        }
        else
            Debug.LogError("There is no save data!");
        return 0;
    }

    public int LoadAppleScore()
    {
        if (File.Exists(Application.persistentDataPath + "/ApplesData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ApplesData.dat", FileMode.Open);
            AppleScoreSave data = (AppleScoreSave)bf.Deserialize(file);
            file.Close();
            return data.appleScore;
        }
        else
            Debug.LogError("There is no save data!");
        return 0;
    }
}
