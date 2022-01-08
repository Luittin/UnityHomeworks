using UnityEngine;

public enum ButtonState
{
    PressDown = 0,
    Hold = 1,
    PressUp = 2,
}

public delegate void AxisHandler(float axisValue);
public delegate void ButtonHandler(ButtonState buttonState);

public delegate void ButtonNumberHandler(int numberButton);

public class InputPlayer : MonoBehaviour
{
    public event AxisHandler OnHorizontalAxis;
    public event AxisHandler OnVerticalAxis;
    public event AxisHandler OnVerticatMouseAxis;
    public event AxisHandler OnHorizontalMouseAxis;
    
    public event AxisHandler OnMouseScrollWheel;

    public event ButtonHandler OnFire;
    public event ButtonHandler OnReload;

    public event ButtonNumberHandler OnNumberButton;

    private int _numberButton = 1;

    public int NumberButton { set { _numberButton = value; OnNumberButton?.Invoke(_numberButton); } }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        OnHorizontalAxis?.Invoke(Input.GetAxis("Horizontal"));
        OnVerticalAxis?.Invoke(Input.GetAxis("Vertical"));
        OnHorizontalMouseAxis?.Invoke(Input.GetAxis("Mouse X"));
        OnVerticatMouseAxis?.Invoke(Input.GetAxis("Mouse Y"));
        OnMouseScrollWheel?.Invoke(Input.GetAxis("Mouse ScrollWheel"));

        ExecuteButtonHandle("Fire1", OnFire);
        ExecuteButtonHandle("Reload", OnReload);

        NumberButtonHandler();
    }

    private void NumberButtonHandler()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NumberButton = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NumberButton = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            NumberButton = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            NumberButton = 3;
        }
    }

    private void ExecuteButtonHandle(string actionName, ButtonHandler handler)
    {
        if (Input.GetButtonDown(actionName))
        {
            handler?.Invoke(ButtonState.PressDown);
        }
        else if (Input.GetButtonUp(actionName))
        {
            handler?.Invoke(ButtonState.PressUp);
        }
        else if (Input.GetButton(actionName))
        {
            handler?.Invoke(ButtonState.Hold);
        }
    }
}
