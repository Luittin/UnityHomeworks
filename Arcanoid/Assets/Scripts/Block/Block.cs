using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private BlockHealth _blockHealth;

    [SerializeField]
    private int _numberPresetEffect;

    public Action<int, Transform> destroyBlock;

    public int NumberPresetEffect { get => _numberPresetEffect; set => _numberPresetEffect = value; }

    public void DestroyBlock()
    {
        destroyBlock?.Invoke(_numberPresetEffect, transform);
        Destroy(this.gameObject);
    }
}
