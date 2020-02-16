using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager
{
    string gamePath = "/gameStats";
    string inputPath = "/inputStats.txt";

    public void SaveGame(GameStats gameStats)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string filePath = Application.persistentDataPath + gamePath + gameStats.saveIndex.ToString() + ".txt";
        FileStream fs = new FileStream(filePath,FileMode.Create);

        SaveGameData saveGameData = new SaveGameData(gameStats);

        bf.Serialize(fs,saveGameData);

        fs.Close();
    }   

    public GameStats LoadGame(int saveIndex)
    {
        string filePath = Application.persistentDataPath + gamePath + saveIndex.ToString() + ".txt";

        if(File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePath,FileMode.Open);

            SaveGameData saveGameData = bf.Deserialize(fs) as SaveGameData;

            fs.Close();

            GameStats loadedData = new GameStats();
            loadedData.Init(saveGameData);

            return loadedData;
        }
        else
        {
            return null;
        }
    }

    public void DeleteGame(GameStats gameStats)
    {
        if(gameStats == null) return;
        string filePath = Application.persistentDataPath + gamePath + gameStats.saveIndex.ToString() + ".txt";

        File.Delete(filePath);
    }

    public void SaveInput(InputManager inputManager)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string filePath = Application.persistentDataPath + inputPath;
        FileStream fs = new FileStream(filePath,FileMode.Create);

        SaveInputData saveInputData = new SaveInputData(inputManager);

        bf.Serialize(fs,saveInputData);

        fs.Close();
    }

    public InputManager LoadInput()
    {
        string filePath = Application.persistentDataPath + inputPath ;

        if(File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePath,FileMode.Open);

            SaveInputData saveInputData = bf.Deserialize(fs) as SaveInputData;

            fs.Close();

            InputManager loadedData = new InputManager();
            loadedData.Init(saveInputData);

            return loadedData;
        }
        else
        {
            return null;
        }
    }
}
