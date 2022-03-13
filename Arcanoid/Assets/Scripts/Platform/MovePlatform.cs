using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private InputHandler _inputHandler;
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private float _trafficLimiter = 1.4f;

    private float _direction = 0.0f;

    private void Awake()
    {
        _inputHandler.HorizontalHandler += OnDirection;
    }

    private void Update()
    {
        Vector2 position = transform.position;

        position.x += _direction * _speed * Time.deltaTime;

        if(position.x >= _trafficLimiter)
        {
            position.x = _trafficLimiter;
        }
        else if(position.x <= -_trafficLimiter)
        {
            position.x = -_trafficLimiter;
        }       

        transform.position = position;    
    }

    public void OnDirection(float direction)
    {
        _direction = direction;
    }
}
