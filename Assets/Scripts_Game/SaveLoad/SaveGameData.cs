using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData 
{
    public int deathCount;
    public int saveIndex;
    public int trophyCount;
    public List<int> completedLevels;
    public int currentLevel;
    public List<int> uncompletedLevels;

    public SaveGameData(GameStats gameStats_)
    {
        deathCount = gameStats_.deathCount;
        saveIndex = gameStats_.saveIndex;
        trophyCount = gameStats_.trophyCount;
        completedLevels = gameStats_.completedLevels;
        currentLevel = gameStats_.currentLevel;
        uncompletedLevels = gameStats_.uncompletedLevels;
    }   
}


