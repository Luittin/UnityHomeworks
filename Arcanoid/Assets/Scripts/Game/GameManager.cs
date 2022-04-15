using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager = null;
    
    private SaveSystem _saveSystem;
    
    private GameData _gameData;

    private LevelSetting _levelSetting;

    public GameData GameData
    {
        get => _gameData;
        set => _gameData = value;
    }

    public LevelSetting LevelSetting
    {
        get => _levelSetting;
        set => _levelSetting = value;
    }

    private void Awake()
    {
        if (_gameManager != null)
        {
            Destroy(gameObject);
        }

        _gameManager = this;
        
        _saveSystem = new SaveSystem();
        _gameData = _saveSystem.Load();
        _levelSetting = new LevelSetting();
    }

    public static GameManager Instantiate()
    {
        return _gameManager;
    }

    public void SaveGame()
    {
        _saveSystem.Save(_gameData);
    }
}
