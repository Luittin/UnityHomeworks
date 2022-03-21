using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _maxLives = 3;
    [SerializeField]
    private int _countLives = 3;

    [SerializeField]
    private Vector2 _startFieldPoin;

    [SerializeField]
    private Background background;
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

    private void Awake()
    {
        LevelSetting levelSetting = LevelSetting.Instantiate();
        _level = LoaderAssets<LevelObject>.GetAsset($"Assets/Resources/Chapters/Chapter{levelSetting.ChapterNumber}/Level{levelSetting.LevelNumber}.asset");
        _chapter = _level._chapter;

        FillGamePlane();
    }

    private void FillGamePlane()
    {
        if (_level._background < 0)
        {
            background.SetBackground(_chapter._backgrounds[_level._background]);
        }

        _ballStats.GetComponent<Ball>().OnCollision += _audioManager.OnPlayAudio;

        float stepInstantiateX = 0.8f;
        float stepInstantiateY = 0.5f;
        Debug.Log(stepInstantiateX);

        foreach(FieldSquare fieldSquare in _level._levelSquares)
        { 
                if(fieldSquare.BlockNumber > 0)
                {
                    Vector2 createPosition = new Vector2(_startFieldPoin.x + stepInstantiateX * fieldSquare.Row, _startFieldPoin.y - stepInstantiateY * fieldSquare.Colum);

                    Block block = Instantiate(_chapter._blocks[fieldSquare.BlockNumber - 1]._prefabBlock, createPosition, Quaternion.identity).GetComponent<Block>();
                    
                    if(fieldSquare.EffectNumber > 0)
                    {
                        block.Effect = _chapter._effects[fieldSquare.EffectNumber - 1]._prefubEffect;
                    }
                }
            
        }
    }
}
