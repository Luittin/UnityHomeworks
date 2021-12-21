using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private InputPlayer _inputPlayer;

    [SerializeField, Range(0.0f, 10.0f)]
    private float _speed = 2.0f;
    [SerializeField, Range(0.0f, 200.0f)]
    private float _mouseSense = 100.0f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovementPlayer();

        RotationPlayer();
    }

    private void MovementPlayer()
    {
        float horizontalMove = _inputPlayer.HorizontalPosition;
        float verticalPosition = _inputPlayer.VerticalPosition;

        Vector3 current = Vector3.zero;

        if(horizontalMove > 0.0f)
        {
            current += transform.right;
        }
        else if(horizontalMove < 0.0f)
        {
            current -= transform.right;
        }
        if(verticalPosition > 0.0f)
        {
            current += transform.forward;
        }
        else if(verticalPosition < 0.0f)
        {
            current -= transform.forward;
        }

        _rigidbody.MovePosition(transform.position + current * Time.fixedDeltaTime * _speed);
    }

    private void RotationPlayer()
    {
        float xMousePosition = transform.rotation.eulerAngles.x - _inputPlayer.MousePositionY * _mouseSense * Time.fixedDeltaTime;
        float yMousePosition = transform.rotation.eulerAngles.y + _inputPlayer.MousePositionX * _mouseSense * Time.fixedDeltaTime;
        Quaternion rotate = Quaternion.Euler(xMousePosition, yMousePosition, 0.0f);
        _rigidbody.MoveRotation(rotate);
        
    }
}
