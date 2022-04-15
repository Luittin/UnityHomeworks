using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int _countLife = 3;

    [SerializeField]
    private Vector2 _startFieldPoin;
    
    [SerializeField]
    private Transform _parentBlocks;
    

    [SerializeField]
    private UIGameMenuController _menuController;

    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private MovePlatform _platform;
    [SerializeField]
    private Ball _ball;
    [SerializeField]
    private Background _background;

    [SerializeField]
    private InputTouchHandler _touchHandler;
    [SerializeField]
    private InputJoystickHandler _joystickHandler;
    [SerializeField]
    private BallSight _ballSight;

    [SerializeField]
    private string LEVELS_PATH = "Assets/Resources/Chapters/Chapter{0}/level{1}.asset";
    [SerializeField]
    private string CHAPTER_PATH = "Assets/Resources/ChapterObjekt/Chapter{0}.asset";

    [SerializeField] 
    private State _pauseMenu;
    [SerializeField] 
    private State _endGameMenu;
    
    private GameManager _gameManager;
    private LevelSetting _levelSetting;

    private ChapterObject _chapterPreset;
    private LevelObject _levelPreset;

    private IGenerationBlock _generationBlock;
    private CreateBonusAndEffect _createBonus;
    private CreateBallLevel _createBall;
    
    private void Awake()
    {
        SetInputHandler();

        _gameManager = GameManager.Instantiate();

        if (_gameManager.LevelSetting != null)
        {
            _levelSetting = _gameManager.LevelSetting;
        }
        
        _levelPreset = LoaderAssets<LevelObject>.GetAsset(string.Format(LEVELS_PATH,_levelSetting.ChapterNumber, _levelSetting.LevelNumber));
        _chapterPreset = LoaderAssets<ChapterObject>.GetAsset(string.Format(CHAPTER_PATH, _levelSetting.ChapterNumber));

        GenerationLevel();
    }

    private void SetInputHandler()
    {
        _touchHandler.HorizontalHandler += _joystickHandler.OnDragTouch;
        _touchHandler.TouchUpOrDown += _joystickHandler.OnTouchUpDown;
        _touchHandler.HorizontalHandler += _ballSight.OnDrag;
        _touchHandler.TouchUpOrDown += _ballSight.OnTouchUpDown;

        _joystickHandler.HorizontalHandler += _platform.OnDirection;

        _ballSight.DirectionSight += OnPushBall;        
    }
    
    private void GenerationLevel()
    {
        if(_levelPreset != null)
        {
            _generationBlock = new LevelGenerationBlock(_levelPreset, _chapterPreset, _startFieldPoin, _parentBlocks);
        }
        _createBonus = new CreateBonusAndEffect();
        _createBall = new CreateBallLevel();
        
        FillGamePlane();
    }
    
    public void FillGamePlane()
    {
        if (_levelPreset._background < 0)
        {
            _background.SetBackground(_chapterPreset._backgrounds[_levelPreset._background]);
        }
        
        _createBall.CreateBall(_ball.GetComponent<BallStats>(), this);
        
        _platform._triggerBonus += OnSelectedEffect;
        
        _generationBlock.StartGeneration(this, _createBonus);
    }

    public void OnPushBall(Vector2 direction)
    {
        _ball.StartMoveBall(direction);
        _joystickHandler.AnableHandler();        
    }
    
    public void OnDecreaseLife()
    {
        _countLife--;
        if (_countLife == 0)
        {
            EndGame(_endGameMenu);
        }        
    }

    public void OnDestroyAllBlocks()
    {
        _gameManager.GameData.LevelsDone[_chapterPreset._numberChapter - 1].ComplitedLevels[_levelPreset._levelNumber - 1] = _countLife;
        _gameManager.GameData.LevelsDone[_chapterPreset._numberChapter - 1].ComplitedLevels.Add(0);
        _gameManager.SaveGame();
        
        EndGame(_pauseMenu);
    }
    
    private void EndGame(State state)
    {
        _menuController.OnSelectMenu(state);
    }
    
    public void OnSelectedEffect(TargetEffect target, int numberEffect)
    {
        Transform targetTransform = null;
        
        switch (target)
        {
            case TargetEffect.Platform:
                targetTransform = _platform.transform; 
                break;
            case TargetEffect.Ball:
                targetTransform = _ball.transform;
                break;
        }

        _createBonus.CreateEffect(_chapterPreset, targetTransform, numberEffect);
    }
}

