using UnityEngine;

public class GenerationLevel : MonoBehaviour
{
    [SerializeField]
    private Vector2 _startFieldPoin;

    [SerializeField] 
    private GameManager _gameManager;
    
    public int StartFill(GameManager gameManager)
    {
        _gameManager = gameManager;
        
        return FillGamePlane();
    }

    private int FillGamePlane()
    {
        if (_gameManager.Level._background < 0)
        {
            _gameManager.Background.SetBackground(_gameManager.Chapter._backgrounds[_gameManager.Level._background]);
        }
        
        Ball ball = _gameManager.BallStats.GetComponent<Ball>();
        ball.OnCollision += _gameManager.AudioManager.OnPlayAudio;
        ball.DepartureAbroad += _gameManager.OnDepartureAbroadBall;
        
        _gameManager.PlatformStats.GetComponent<MovePlatform>()._trigerBonus += _gameManager.OnSelectedEffect;
        
        float stepInstantiateX = 0.8f;
        float stepInstantiateY = 0.5f;

        int countBlock = 0;
        
        foreach(FieldSquare fieldSquare in _gameManager.Level._levelSquares)
        {
            if(fieldSquare.BlockNumber > 0)
            {
                Vector2 createPosition = new Vector2(_startFieldPoin.x + stepInstantiateX * fieldSquare.Row, _startFieldPoin.y - stepInstantiateY * fieldSquare.Colum);

                Block block = Instantiate(_gameManager.Chapter._blocks[fieldSquare.BlockNumber - 1]._prefabBlock, createPosition, Quaternion.identity).GetComponent<Block>();

                countBlock++;
                
                if (fieldSquare.EffectNumber > -1)
                {
                    block.NumberPresetEffect = fieldSquare.EffectNumber;
                    block.destroyBlock += _gameManager.OnDestroyBlockFromBonus;
                }
            }
            
        }

        return countBlock;
    }

    public void CreateBonus(int numberPresetEffect, Transform transformBlock)
    {
        GameObject prefabBonus = _gameManager.Chapter._effects[numberPresetEffect]._prefabBonus;
        Bonus bonus = Instantiate(prefabBonus, transformBlock.position, Quaternion.identity).GetComponent<Bonus>();
        bonus.NumberEffect = numberPresetEffect;
    }

    public void CreateEffect(Transform target, int numberEffect)
    {
        Effect effect = Instantiate(_gameManager.Chapter._effects[numberEffect]._prefabEffect, target).GetComponent<Effect>();
        effect.Stats = target.GetComponent<Stats>();
        effect.StartEffect();
    }
    
}