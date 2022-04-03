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
    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private LevelObject _level;
    [SerializeField]
    private ChapterObject _chapter;
    
    public Background Background => _background;
    public BallStats BallStats => _ballStats;
    public PlatformStats PlatformStats => _platformStats;
    public AudioManager AudioManager => _audioManager;
    public LevelObject Level => _level;
    public ChapterObject Chapter => _chapter;
    
    private void Awake()
    {
        LevelSetting levelSetting = LevelSetting.Instantiate();
        _level = LoaderAssets<LevelObject>.GetAsset($"Assets/Resources/Chapters/Chapter{levelSetting.ChapterNumber}/Level{levelSetting.LevelNumber}.asset");
        _chapter = _level._chapter;

        _balls = new List<Ball>();
        _balls.Add(_ballStats.GetComponent<Ball>());
        
        _countBlock = _generationLevel.StartFill(this);
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
            if (_countLives == 0)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;
    }
    
    public void OnDestroyBlockFromBonus(int numberPresetEffect, Transform transformBlock)
    {
        _countBlock--;
        if (_countBlock == 0)
        {
            Time.timeScale = 0;
            return;
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
