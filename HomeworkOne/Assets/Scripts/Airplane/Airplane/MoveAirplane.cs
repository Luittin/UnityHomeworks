using System;
using UnityEngine;

public class MoveAirplane : MonoBehaviour
{
    [SerializeField,Range(0.0f,10.0f)]
    private float _stepSpeed = 1.0f;
    [SerializeField]
    private float _currentSpeed = 0.0f;
    [SerializeField]
    private float _maxSpeed = 5.0f;

    [SerializeField, Range(0.0f,50.0f)]
    private float _speedTurn = 10.0f;
    [SerializeField]
    private float _maxBank = 35.0f;
    [SerializeField, Range(0.0f, 10.0f)]
    private float _stepBank = 1.0f;

    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _playerInputHandler = GameObject.FindObjectOfType<PlayerInputHandler>();
    }

    private void Update()
    {
        float xMousePosition = transform.rotation.eulerAngles.x - _playerInputHandler.MousePosition;
        

        float horizontalRotation = _playerInputHandler.HorizontalMove;
        float yRotation = transform.rotation.eulerAngles.y + (horizontalRotation != 0.0f ? Mathf.Sign(horizontalRotation) : 0.0f) * _speedTurn * Time.deltaTime;
        
        float zRotation = MoveBank(horizontalRotation);

        Quaternion rotate = Quaternion.Euler(xMousePosition, yRotation, zRotation);
        
        transform.rotation = rotate;

        MovmentAirplane();
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }

    private void MovmentAirplane()
    {
        if (_playerInputHandler.VerticalMove != 0.0f)
            _currentSpeed = Mathf.Clamp(_currentSpeed + _stepSpeed * Mathf.Sign(_playerInputHandler.VerticalMove), -_maxSpeed, _maxSpeed);
        if (_playerInputHandler.VerticalMove == 0.0f && _currentSpeed != 0.0f)
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0.0f, _stepSpeed);
    }

    private float MoveBank(float horizontalRotation)
    {
        float currentRotation = transform.rotation.eulerAngles.z > 320.0f ? transform.rotation.eulerAngles.z -360.0f : transform.rotation.eulerAngles.z;

        if(horizontalRotation != 0.0f)
            currentRotation = Mathf.Clamp(currentRotation - _stepBank * Mathf.Sign(horizontalRotation), -35.0f, 35.0f);
        if (horizontalRotation == 0.0f && currentRotation != 0.0f)
            currentRotation = Mathf.MoveTowards(currentRotation, 0.0f, _stepBank);

        return currentRotation;
    }
}
