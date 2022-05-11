using System;
using UnityEngine;

public class BlockStats : Stats
{
    [SerializeField]
    private int _health = 1;
    [SerializeField]
    private int _numberPresetEffect;
    [SerializeField]
    private Sprite _sprite;

    public event Action ResetSprite;


    public int Health { get => _health; set => _health = value; }
    public bool Invulnerability { get; set; } = false;
    public int NumberPresetEffect { get => _numberPresetEffect; set => _numberPresetEffect = value; }
    public Sprite Sprite { get => _sprite; set { _sprite = value; ResetSprite?.Invoke(); } }

    private void Awake()
    {
        Block block = GetComponent<Block>();
        ResetSprite = block.OnResetSpriteBlock;
    }
}
