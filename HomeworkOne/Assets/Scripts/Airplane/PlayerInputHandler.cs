using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private float _horizontalMove = 0.0f;
    [SerializeField]
    private float _verticalMove = 0.0f;
    [SerializeField]
    private float _forceSpeed = 0.0f;
    [SerializeField]
    private bool _fire = false;

    [SerializeField]
    private float _mousePosition = 0.0f;

    public float HorizontalMove { get => _horizontalMove; }
    public float VerticalMove { get => _verticalMove; }
    public float ForceSpeed { get => _forceSpeed; }
    public bool Fire { get => _fire; }
    public float MousePosition { get => _mousePosition; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");

        _fire = Input.GetButton("Fire1");

        _forceSpeed = Input.GetAxis("ForceSpeed");

        _mousePosition = Input.GetAxis("Mouse Y");
    }
}
