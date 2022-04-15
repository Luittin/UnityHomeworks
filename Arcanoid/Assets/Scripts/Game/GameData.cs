using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField]
    private GameSetting _gameSetting;

    [SerializeField]
    private List<FinishChapter> _levelsDone;

    public GameSetting GameSetting { get => _gameSetting; set => _gameSetting = value; }

    public List<FinishChapter> LevelsDone
    {
        get
        {
            return _levelsDone;
        }
        set
        {
            if (_levelsDone == null)
            {
                _levelsDone = value;
            }
        }
    }

    
}
