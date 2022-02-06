using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private IHandler _inputHandler;

    [SerializeField]
    private float _moveSpeed = 2.0f;
    [SerializeField]
    private float _jumpForce = 10.0f;
    [SerializeField]
    private float _mouseSensitivity = 5.0f;

    private CharacterController _controller;
    private Vector3 playerVelocity;

    private bool groundedPlayer;

    private float _gravityValue = -9.81f;

    private float horizontalAxis = 0.0f;
    private float verticalAxis = 0.0f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _inputHandler = GetComponent<IHandler>();

        _inputHandler.OnHorizontalAxis += OnHorizontalDirection;
        _inputHandler.OnVerticalAxis += OnVerticalDirection;

        _inputHandler.OnJump += OnJump;

        _inputHandler.OnHorizontalMouseAxis += OnHorizontalMouseRotation;
    }

    private void Update()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(horizontalAxis, 0, verticalAxis);
        _controller.Move(move * Time.deltaTime * _moveSpeed);

        VerticalVelocity();
    }

    private void VerticalVelocity()
    {
        playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
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

    public void OnJump(ButtonState buttonState)
    {
        if(buttonState == ButtonState.PressDown && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpForce * -3.0f * _gravityValue);
            VerticalVelocity();
        }
    }
}
