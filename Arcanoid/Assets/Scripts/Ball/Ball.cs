using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
class Ball : MonoBehaviour
{
    [SerializeField]
    public float _speedBall;

    [SerializeField]
    private BallDirection _ballDirection;

    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private Rigidbody _rigidbody;

    private bool isStartMove = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        
    }

    private void FixedUpdate()
    {
        if(isStartMove == false)
        {
            return;
        }

        _rigidbody.AddForce(_ballDirection.Direction, ForceMode.Impulse);
    }

    public void OnstartMoveBall()
    {
        isStartMove = true;
    }

    public void OnstopMoveBall()
    {
        isStartMove = false;
    }
}
