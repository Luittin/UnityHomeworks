using UnityEngine;

public enum ButtonStatus
{
    ButtonDown = 0,
    ButtonHold = 1,
    ButtonUp = 2
}

public delegate void AxisHandler(float axisValue);
public delegate void ButtonHandler(ButtonStatus buttonStatus);

public class PCInputHandler : MonoBehaviour
{
    public event AxisHandler VerticalAxis;
    public event AxisHandler HorizontalAxis;

    public event AxisHandler VerticalMauseAxis;
    public event AxisHandler HorizontalMauseAxis;

    public event ButtonHandler MouseLeft;
    public event ButtonHandler MouseHard;

    private void Update()
    {
        VerticalAxis?.Invoke(Input.GetAxisRaw("Vertical"));
        HorizontalAxis?.Invoke(Input.GetAxisRaw("Horizontal"));

        VerticalMauseAxis?.Invoke(Input.GetAxisRaw("Mouse Y"));
        HorizontalMauseAxis?.Invoke(Input.GetAxisRaw("Mouse X"));

        ExecuteButtonHandle("Fire1", MouseLeft);
        ExecuteButtonHandle("Fire2", MouseHard);
    }

    private void ExecuteButtonHandle(string actionName, ButtonHandler handler)
    {
        if (Input.GetButtonDown(actionName))
        {
            handler?.Invoke(ButtonStatus.ButtonDown);
        }
        else if (Input.GetButtonUp(actionName))
        {
            handler?.Invoke(ButtonStatus.ButtonUp);
        }
        else if (Input.GetButton(actionName))
        {
            handler?.Invoke(ButtonStatus.ButtonHold);
        }
    }
}
