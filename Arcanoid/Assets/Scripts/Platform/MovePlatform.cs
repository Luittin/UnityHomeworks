using UnityEngine;

[RequireComponent(typeof(PlatformStats))]
public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private InputHandler _inputHandler;
    [SerializeField]
    private PlatformStats _platformStats;

    private float _direction = 0.0f;

    private void Awake()
    {
        _inputHandler.HorizontalHandler += OnDirection;
    }

    private void Update()
    {
        Vector2 position = transform.position;

        position.x += _direction * _platformStats.Speed * Time.deltaTime;

        if(position.x >= _platformStats.TrafficLimiter)
        {
            position.x = _platformStats.TrafficLimiter;
        }
        else if(position.x <= -_platformStats.TrafficLimiter)
        {
            position.x = -_platformStats.TrafficLimiter;
        }       

        transform.position = position;    
    }

    public void OnDirection(float direction)
    {
        _direction = direction;
    }
}
