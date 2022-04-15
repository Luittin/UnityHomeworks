using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    private SaveOrLoad _save;

    public SaveSystem()
    {
        _save = new SaveThroughPlayerPrefs();
    }
    
    public void Save(GameData gameData)
    {
        _save.Save(gameData);
    }

    public GameData Load()
    {
        GameData gameData = _save.Load();
        
        if(gameData == null)
        {
            gameData = LoadNewGameData();
        }
        
        return gameData;
    }

    private GameData LoadNewGameData()
    {
        GameData gameData = new GameData();

        gameData.LevelsDone = new List<FinishChapter>();
        gameData.LevelsDone.Add(new FinishChapter());
        gameData.LevelsDone[0].ComplitedLevels = new List<int>();
        gameData.LevelsDone[0].ComplitedLevels.Add(0);

        gameData.GameSetting = new GameSetting();
        gameData.GameSetting.MusicVolume = 0.5f;
        gameData.GameSetting.SoundVolume = 0.5f;

        return gameData;
    }
}
