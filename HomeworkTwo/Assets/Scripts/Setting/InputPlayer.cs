using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    private float _verticalPosition;
    [SerializeField]
    private float _horizontalPosition;
    [SerializeField]
    private float _mousePositionX;
    [SerializeField]
    private float _mousePositionY;
    [SerializeField]
    private int _selectGun = 1;
    [SerializeField]
    private bool _lightShooting = false;

    public float VerticalPosition { get => _verticalPosition; }
    public float HorizontalPosition { get => _horizontalPosition; }
    public float MousePositionX { get => _mousePositionX; }
    public bool LightShooting { get => _lightShooting; }
    public float MousePositionY { get => _mousePositionY; }
    public int SelectGun { get => _selectGun; set => _selectGun = value; }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _verticalPosition = Input.GetAxis("Vertical");
        _horizontalPosition = Input.GetAxis("Horizontal");

        _mousePositionX = Input.GetAxis("Mouse X");
        _mousePositionY = Input.GetAxis("Mouse Y");

        _lightShooting = Input.GetButton("Fire1");

        if (Input.GetButtonDown("1"))
        {
            _selectGun = 1;
        }

        if (Input.GetButtonDown("2"))
        {
            _selectGun = 2;
        }
    }
}
