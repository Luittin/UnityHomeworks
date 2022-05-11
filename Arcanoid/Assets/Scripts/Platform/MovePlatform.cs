using System;
using UnityEngine;

[RequireComponent(typeof(PlatformStats))]
public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private PlatformStats _platformStats;

    [SerializeField]
    private Vector2 _startPosition;
    
    private float _direction = 0.0f;

    public event Action<TargetEffect, int> TriggerBonus;

    private void Update()
    {
        Vector2 position = transform.position;

        position.x += _direction * _platformStats.Speed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -_platformStats.TrafficLimiter, _platformStats.TrafficLimiter);      

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Bonus>() != null)
        {
            Bonus bonus = col.GetComponent<Bonus>();

            TriggerBonus?.Invoke(bonus.TargetEffect,bonus.NumberEffect);
            Destroy(bonus.gameObject);
        }        
    }
}
