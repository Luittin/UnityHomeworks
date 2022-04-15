using System;
using UnityEngine;

public class BlockStats : Stats, IPoolable
{
    [SerializeField]
    private int _health = 1;
    [SerializeField]
    private int _numberPresetEffect;
    [SerializeField]
    private Sprite _sprite;

    public Action resetSprite;


    private bool _invulnerability = false;

    public int Health { get => _health; set => _health = value; }
    public bool Invulnerability { get => _invulnerability; set => _invulnerability = value; }
    public int NumberPresetEffect { get => _numberPresetEffect; set => _numberPresetEffect = value; }
    public Sprite Sprite { get => _sprite; set { _sprite = value; resetSprite?.Invoke(); } }

    public void ReturnToPool()
    {
        throw new NotImplementedException();
    }

    public void RequestFromPool()
    {
        throw new NotImplementedException();
    }
}
