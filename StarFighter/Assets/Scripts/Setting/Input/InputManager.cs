using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InputManager : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)]
    private float _stepSpeed = 1.0f;
    [SerializeField]
    private float _currentSpeed = 0.0f;
    [SerializeField]
    private float _maxSpeed = 5.0f;

    private Rigidbody _rigidbody;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private float mouseHorizontal = 0.0f;
    private float mouseVertical = 0.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        var inputHandler = GameObject.FindObjectOfType<PCInputHandler>();
        inputHandler.VerticalAxis += OnVerticalAxis;
        inputHandler.HorizontalAxis += OnHorizontalAxis;
        inputHandler.VerticalMauseAxis += OnMauseVerticalAxis;
        inputHandler.HorizontalMauseAxis += OnMauseHorizontalAxis;
    }

    private void Update()
    {
        transform.Rotate(0.0f, 0.0f, horizontal, Space.Self);

        transform.Rotate(-mouseVertical, mouseHorizontal, 0.0f, Space.World);

        MovmentAirplane();
        _rigidbody.AddForce(transform.forward * _currentSpeed * Time.deltaTime, ForceMode.Force);
    }

    private void MovmentAirplane()
    {
        if (vertical != 0.0f)
            _currentSpeed = Mathf.Clamp(_currentSpeed + _stepSpeed * Mathf.Sign(vertical), -_maxSpeed, _maxSpeed);
        if (vertical == 0.0f && _currentSpeed != 0.0f)
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0.0f, _stepSpeed);
    }

    public void OnVerticalAxis(float axisValue)
    {
        vertical = axisValue;
    }

    public void OnHorizontalAxis(float axisValue)
    {
        horizontal = axisValue;
    }

    public void OnMauseVerticalAxis(float axisValue)
    {
        mouseVertical = axisValue;
    }

    public void OnMauseHorizontalAxis(float axisValue)
    {
        mouseHorizontal = axisValue;
    }
}
