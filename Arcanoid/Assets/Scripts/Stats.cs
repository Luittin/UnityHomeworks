using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private float _size = 1.0f;

    public Action _changedSize;

    public float Speed { get => _speed; set => _speed = value; }
    public float Size { get => _size; set { _size = value; _changedSize?.Invoke(); } }
}
