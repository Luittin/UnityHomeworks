using System;
using UnityEngine;

[RequireComponent(typeof(BlockStats))]
public class Block : MonoBehaviour
{
    [SerializeField]
    private BlockStats _blockStats;

    private SpriteRenderer spriteRenderer;

    public event Action<BlockStats> DestroyBlock;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        _blockStats = GetComponent<BlockStats>();
    }

    public void DecrementHealth(int damage)
    {
        if (_blockStats.Invulnerability) return;
        
        _blockStats.Health -= damage;

        if (_blockStats.Health <= 0)
        {
            DestructionBlock();
        }
    }
    
    private void DestructionBlock()
    {
        DestroyBlock?.Invoke(_blockStats);
        Destroy(gameObject);
    }

    public void OnResetSpriteBlock()
    {
        spriteRenderer.sprite = _blockStats.Sprite;
    }
}
