using UnityEngine;
[RequireComponent(typeof(CharacterController), typeof(HealthPlayer))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private InputPlayer _inputPlayer;

    [SerializeField]
    private Transform _upperBody;

    [SerializeField]
    private float _moveSpeed = 2.0f;
    [SerializeField]
    private float _mouseSensitivity = 5.0f;

    private CharacterController _controller;

    private float horizontalAxis = 0.0f;
    private float verticalAxis = 0.0f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _inputPlayer.OnHorizontalAxis += OnHorizontalDirection;
        _inputPlayer.OnVerticalAxis += OnVerticalDirection;

        _inputPlayer.OnHorizontalMouseAxis += OnHorizontalMouseRotation;
        _inputPlayer.OnVerticatMouseAxis += OnVerticalMouseRotation;
    }

    private void Update()
    {
        Vector3 directionMove = Vector3.zero;

        if (horizontalAxis > 0.0f)
        {
            directionMove += transform.right;
        }
        else if (horizontalAxis < 0.0f)
        {
            directionMove -= transform.right;
        }

        if (verticalAxis > 0.0f)
        {
            directionMove += transform.forward;
        }
        else if (verticalAxis < 0.0f)
        {
            directionMove -= transform.forward;
        }
        directionMove.y -= 9.8f;
        _controller.Move(directionMove * Time.fixedDeltaTime * _moveSpeed);
    }

    public void OnHorizontalDirection(float axisValue)
    {
        horizontalAxis = axisValue;
    }

    public void OnVerticalDirection(float axisValue)
    {
        verticalAxis = axisValue;
    }

    public void OnHorizontalMouseRotation(float axisValue)
    {
        float yMousePosition = transform.rotation.eulerAngles.y + axisValue * _mouseSensitivity * Time.fixedDeltaTime;
        Quaternion rotate = Quaternion.Euler(0.0f, yMousePosition, 0.0f);
        transform.rotation = rotate;
    }

    public void OnVerticalMouseRotation(float axisValue)
    {
        float xMousePosition = _upperBody.localRotation.eulerAngles.x - axisValue * _mouseSensitivity * Time.fixedDeltaTime;
        Quaternion rotate = Quaternion.Euler(xMousePosition, 0.0f, 0.0f);
        _upperBody.localRotation = rotate;
    }
}
