using System;
using UnityEngine;

[RequireComponent(typeof(PlatformStats))]
public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private InputHandler _inputHandler;
    [SerializeField]
    private PlatformStats _platformStats;

    [SerializeField]
    private Vector2 _startPosition;
    
    private float _direction = 0.0f;

    public Action<TargetEffect, int> _trigerBonus;

    private void Awake()
    {
        _inputHandler.HorizontalHandler += OnDirection;
    }

    private void Update()
    {
        Vector2 position = transform.position;

        position.x += _direction * _platformStats.Speed * Time.deltaTime;

        Mathf.Clamp(position.x, -_platformStats.TrafficLimiter, _platformStats.TrafficLimiter);      

        transform.position = position;    
    }

    public void MoveStartPosition()
    {
        transform.position = _startPosition;
    }
    
    public void OnDirection(float direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bonus"))
        {
            Bonus bonus = other.GetComponent<Bonus>();
            _trigerBonus?.Invoke(bonus.TargetEffect,bonus.NumberEffect);
            Destroy(bonus.gameObject);
        }
    }
}
