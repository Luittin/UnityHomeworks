using UnityEngine;

public delegate void AxisHandler(float axisValue);
public delegate void ButtonHandler();

public class InputHandler : MonoBehaviour
{
    public event AxisHandler HorizontalHandler;

    public event ButtonHandler PressstartHandler;

    private void Update()
    {
        HorizontalHandler?.Invoke(Input.GetAxis("Horizontal"));
        
        if (Input.GetButtonDown("Space"))
        {
            PressstartHandler?.Invoke();
        }
    }

}
