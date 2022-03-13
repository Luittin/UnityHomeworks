using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BallStats))]
class Ball : MonoBehaviour
{
    [SerializeField]
    private BallStats _ballStats;

    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    private bool isStartMove = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _ballStats = GetComponent<BallStats>();

        _inputHandler.PressstartHandler += OnstartMoveBall;
    }

    public void OnstartMoveBall()
    {
        if (isStartMove == false)
        {
            _rigidbody.AddForce(_ballStats.Direction * _ballStats.Speed, ForceMode2D.Impulse);
        }
        isStartMove = true;
    }

    public void OnstopMoveBall()
    {
        isStartMove = false;
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionEnter(collision.gameObject);
    }

    private void CollisionEnter(GameObject collision)
    {
        if (collision.CompareTag("Block"))
        {
            collision.GetComponent<BlockHealth>().DecrementHealth(_ballStats.Damage);
        }
    }
}
