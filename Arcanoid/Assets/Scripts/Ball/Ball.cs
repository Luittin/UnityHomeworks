using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BallStats))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private BallStats _ballStats;

    [SerializeField]
    private Vector2 _startPosition;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    public event Action<AudioClip> OnCollision;
    public event Action<Ball> DepartureAbroad;
    
    private bool _isStartMove = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _ballStats = GetComponent<BallStats>();
    }

    public void StartMoveBall(Vector2 direction)
    {
        if (_isStartMove == false)
        {
            _rigidbody.AddForce(direction * _ballStats.Speed, ForceMode2D.Impulse);
        }
        _isStartMove = true;
    }

    public void StopBall()
    {
        _isStartMove = false;
        _rigidbody.velocity = Vector2.zero;
    }

    public void MoveStartPosition()
    {
        transform.position = _startPosition;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Stats>() != null)
        {
            //OnCollision?.Invoke(collision.gameObject.GetComponent<Stats>().AudioClip);
        }

        CollisionEnter(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionEnter(collision.gameObject);
    }

    private void CollisionEnter(GameObject collision)
    {
        if (collision.GetComponent<Block>() != null)
        {
            collision.GetComponent<Block>().DecrementHealth(_ballStats.Damage);
        }
        else if (collision.CompareTag("BallCatcher"))
        {
            DepartureAbroad?.Invoke(this);
        }
    }
}
