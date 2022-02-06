using UnityEngine;

public enum ButtonState
{
    PressDown = 0,
    Hold = 1,
    PressUp = 2
}

public delegate void AxisHandler(float axisValue);
public delegate void ButtonHandler(ButtonState buttonState);

public class InputManager : MonoBehaviour, IHandler
{
    private static InputManager inputManager;

    public static InputManager Instance { get => inputManager; }

    public event AxisHandler OnHorizontalAxis;
    public event AxisHandler OnVerticalAxis;
    public event AxisHandler OnVerticatMouseAxis;
    public event AxisHandler OnHorizontalMouseAxis;
    public event ButtonHandler OnJump;

    private void Awake()
    {
        if(inputManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            inputManager = this;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        OnHorizontalAxis?.Invoke(Input.GetAxis("Horizontal"));
        OnVerticalAxis?.Invoke(Input.GetAxis("Vertical"));
        OnHorizontalMouseAxis?.Invoke(Input.GetAxis("Mouse X"));
        OnVerticatMouseAxis?.Invoke(Input.GetAxis("Mouse Y"));

        ExecuteButtonHandle("Jump", OnJump);
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
