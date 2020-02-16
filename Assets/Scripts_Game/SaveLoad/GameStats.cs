using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    public int deathCount;
    public int saveIndex;
    public int trophyCount;
    public List<int> completedLevels;
    public int currentLevel;
    public List<int> uncompletedLevels;

    public void Init(SaveGameData saveGameData)
    {
        deathCount = saveGameData.deathCount;
        saveIndex = saveGameData.saveIndex;
        trophyCount = saveGameData.trophyCount;
        completedLevels = saveGameData.completedLevels;
        currentLevel = saveGameData.currentLevel;

        uncompletedLevels = saveGameData.uncompletedLevels;
    }

    public void InitNewGame(int currentIndex)
    {
        deathCount = 0;
        saveIndex = currentIndex;
        trophyCount = 0;
        completedLevels = new List<int>();
        currentLevel = 1;
                
        uncompletedLevels = new List<int>();
        for(int i=1;i<=10;i++)
        {
            uncompletedLevels.Add(i);
        }
    }

    
}
