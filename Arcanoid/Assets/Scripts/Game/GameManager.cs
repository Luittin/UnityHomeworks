using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _maxLives = 3;
    [SerializeField]
    private int _countLives = 3;
    [SerializeField]
    private int _countBlock;

    [SerializeField]
    private List<Ball> _balls;

    [SerializeField] 
    private GenerationLevel _generationLevel;
    
    [SerializeField]
    private Background _background;
    [SerializeField]
    private BallStats _ballStats;
    [SerializeField]
    private PlatformStats _platformStats;
    private MovePlatform _movePlatform;
    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField] 
    private UIGameMenuController _menuController;
    
    [SerializeField] 
    private InputTouchHandler _touchHandler;
    [SerializeField] 
    private InputJoystickHandler _joystickHandler;
    [SerializeField] 
    private BallSight _ballSight;
    
    [SerializeField]
    private LevelObject _level;
    [SerializeField]
    private ChapterObject _chapter;

    private bool isMoveBall = false;
    
    public Background Background => _background;
    public BallStats BallStats => _ballStats;
    public PlatformStats PlatformStats => _platformStats;
    public AudioManager AudioManager => _audioManager;
    public LevelObject Level => _level;
    public ChapterObject Chapter => _chapter;
    
    private void Awake()
    {
        _menuController.ShowSight();
        
        _movePlatform = _platformStats.GetComponent<MovePlatform>();
        
        _balls = new List<Ball>();
        _balls.Add(_ballStats.GetComponent<Ball>());
        
         
        _touchHandler.HorizontalHandler += _joystickHandler.OnDragTouch;
        _touchHandler.HorizontalHandler += _ballSight.OnDrag;
                
        _touchHandler.TouchUpOrDown += _joystickHandler.OnTouchUpDown;
        _touchHandler.TouchUpOrDown += _ballSight.OnTouchUpDown;
                
        _ballSight.DirectionSight += OnPushBall;
        
        LevelSetting levelSetting = LevelSetting.Instantiate();
        _level = LoaderAssets<LevelObject>.GetAsset($"Assets/Resources/Chapters/Chapter{levelSetting.ChapterNumber}/Level{levelSetting.LevelNumber}.asset");
        _chapter = _level._chapter;
        _countBlock = _generationLevel.StartFill(this);
    }

    public void OnPushBall(Vector2 direction)
    {
        if (!isMoveBall)
        {
            _balls[0].StartMoveBall(direction);
            _menuController.ShowTouch();
            _joystickHandler.HorizontalHandler += _movePlatform.OnDirection;
            _ballSight.DirectionSight -= OnPushBall;
        }
    }
    
    public void OnDepartureAbroadBall(Ball ball)
    {
        if (_balls.Count > 1)
        {
            ball.StopBall();
            Destroy(ball.gameObject);
        }
        else
        {
            ball.StopBall();
            ball.MoveStartPosition();
            _countLives--;
            
            _ballSight.DirectionSight += OnPushBall;
            _joystickHandler.HorizontalHandler -= _movePlatform.OnDirection;
            _menuController.ShowSight();
            
            if (_countLives == 0)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {

        _menuController.OpenPauseMenu();
        Time.timeScale = 0;
    }
    
    public void OnDestroyBlockFromBonus(int numberPresetEffect, Transform transformBlock)
    {
        _countBlock--;
        if (_countBlock == 0)
        {
            //move to a separate method for processing
            LevelSetting.Instantiate().GameData.LevelsDone[_level._chapter._numberChapter - 1].ComplitedLevels[_level._levelNumber - 1] = _countLives;
            LevelSetting.Instantiate().GameData.LevelsDone[_level._chapter._numberChapter - 1].ComplitedLevels.Add(0);
            EndGame();
        }
        _generationLevel.CreateBonus(numberPresetEffect,transformBlock);
    }
    
    public void OnSelectedEffect(TargetEffect target, int numberEffect)
    {
        Transform targetTransform = null;
        
        switch (target)
        {
            case TargetEffect.Platform:
                targetTransform = _platformStats.transform; 
                break;
            case TargetEffect.Ball:
                targetTransform = _ballStats.transform;
                break;
        }

        _generationLevel.CreateEffect(targetTransform, numberEffect);
    }
}
