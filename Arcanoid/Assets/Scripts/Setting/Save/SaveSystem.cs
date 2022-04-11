using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static SaveSystem _saveSystem = null;

    [SerializeField]
    private SaveOrLoad _save;

    private void Awake()
    {
        if (_saveSystem == null)
        {
            _saveSystem = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static SaveSystem Instantiate()
    {
        return _saveSystem;
    }

    public void Save()
    {
        _save.Save(LevelSetting.Instantiate().GameData);
    }

    public void Load()
    {
        LevelSetting.Instantiate().GameData = _save.Load();
        Debug.Log(LevelSetting.Instantiate().GameData);
        if(LevelSetting.Instantiate().GameData == null)
        {
            LoadNewGamedata();
        }
    }

    private void LoadNewGamedata()
    {
        LevelSetting.Instantiate().GameData = new GameData();
        GameData gameData = LevelSetting.Instantiate().GameData;

        gameData.LevelsDone = new List<FinishChapter>();
        gameData.LevelsDone.Add(new FinishChapter());
        gameData.LevelsDone[0].ComplitedLevels = new List<int>();
        gameData.LevelsDone[0].ComplitedLevels.Add(0);

        gameData.GameSetting = new GameSetting();
        gameData.GameSetting.MusicVolume = 0.5f;
        gameData.GameSetting.SoundVolume = 0.5f;

        Save();
    }
}
