using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationBlock : IGenerationBlock
{
    private LevelObject _levelPreset;
    private ChapterObject _chapterPreset;
    private Vector2 _startPosition;
    private Transform _parentBlocks;
    
    private List<BlockStats> _blocks;

    private event Action EndGame;
    private event Action<ChapterObject, int, Transform> DestroyBlock;
    
    public LevelGenerationBlock(LevelObject levelPreset, ChapterObject chapterPreset, Vector2 startPosition, Transform parentBlocks)
    {
        _levelPreset = levelPreset;
        _chapterPreset = chapterPreset;
        _startPosition = startPosition;
        _parentBlocks = parentBlocks;
    }

    public void StartGeneration(LevelManager levelManager, CreateBonus createBonus, CreateEffect createEffect)
    {
        DestroyBlock = createBonus.InstantiateBonus;
        EndGame = levelManager.OnDestroyAllBlocks;
        GenerationBlocs();
    }

    private void GenerationBlocs()
    {
        float stepInstantiateX = 0.8f;
        float stepInstantiateY = 0.5f;

        _blocks = new List<BlockStats>();

        foreach (FieldSquare fieldSquare in _levelPreset._levelSquares)
        {
            if (fieldSquare.BlockNumber > 0)
            {
                Vector2 createPosition = new Vector2(_startPosition.x + stepInstantiateX * fieldSquare.Colum, _startPosition.y - stepInstantiateY * fieldSquare.Row);

                BlockStats block = MonoBehaviour.Instantiate(_chapterPreset._blocks[fieldSquare.BlockNumber - 1]._prefabBlock, createPosition, Quaternion.identity, _parentBlocks).GetComponent<BlockStats>();

                if (fieldSquare.EffectNumber > 0)
                {
                    block.NumberPresetEffect = fieldSquare.EffectNumber;
                    block.GetComponent<Block>().DestroyBlock += OnDestroyBlockFromBonus;
                }

                _blocks.Add(block);
            }
        }
    }

    public void OnDestroyBlockFromBonus(BlockStats blockStats)
    {
        _blocks.Remove(blockStats);

        if(_blocks.Count == 0)
        {
            EndGame?.Invoke();
        }
        
        DestroyBlock?.Invoke(_chapterPreset, blockStats.NumberPresetEffect, blockStats.transform);
    }


}
