using System;
using UnityEngine;

[RequireComponent(typeof(BlockStats))]
public class Block : MonoBehaviour
{
    [SerializeField]
    private BlockStats _blockStats;

    private SpriteRenderer spriteRenderer;

    public Action<BlockStats> destroyBlock;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        _blockStats = GetComponent<BlockStats>();
        _blockStats.resetSprite = OnResetSpriteBlock;
    }

    public void DecrementHealth(int damage)
    {
        if (!_blockStats.Invulnerability)
        {
            _blockStats.Health -= damage;

            if (_blockStats.Health <= 0)
            {
                DestroyBlock();
            }
        }
    }
    
    public void DestroyBlock()
    {
        destroyBlock?.Invoke(_blockStats);
        Destroy(this.gameObject);
    }

    public void OnResetSpriteBlock()
    {
        spriteRenderer.sprite = _blockStats.Sprite;
    }
}
