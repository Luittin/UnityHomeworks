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
    private bool _lightShooting = false;
    [SerializeField]
    private bool _hardShooting = false;

    public float VerticalPosition { get => _verticalPosition; }
    public float HorizontalPosition { get => _horizontalPosition; }
    public float MousePositionX { get => _mousePositionX; }
    public bool LightShooting { get => _lightShooting; }
    public bool HardShooting { get => _hardShooting; }
    public float MousePositionY { get => _mousePositionY; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        _verticalPosition = Input.GetAxis("Vertical");
        _horizontalPosition = Input.GetAxis("Horizontal");

        _mousePositionX = Input.GetAxis("Mouse X");
        _mousePositionY = Input.GetAxis("Mouse Y");

        _lightShooting = Input.GetButton("Fire1");

        if (Input.GetButtonDown("Fire2"))
        {
            _hardShooting = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            _hardShooting = false;
        }
    }
}
