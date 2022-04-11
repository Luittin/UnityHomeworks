using UnityEngine;

public class LevelSetting
{
    private static LevelSetting levelSetting = null;

    private int _levelNumber;
    private int _chapterNumber;

    private GameData _gameData;

    private GameObject _platform;
    private GameObject _ball;

    private LevelSetting()
    {

    }

    public int LevelNumber { get => _levelNumber; set => _levelNumber = value; }
    public int ChapterNumber { get => _chapterNumber; set => _chapterNumber = value; }
    public GameObject Platform { get => _platform; set => _platform = value; }
    public GameObject Ball { get => _ball; set => _ball = value; }
    public GameData GameData 
    {
        get 
        { 
            if(_gameData == null)
            {
                SaveSystem.Instantiate().Load();
            }
            return _gameData; 
        }
        set 
        { 
            _gameData = value; 
        }
    }

    public static LevelSetting Instantiate()
    {
        if(levelSetting == null)
        {
            return levelSetting = new LevelSetting();
        }

        return levelSetting;
    }
}
