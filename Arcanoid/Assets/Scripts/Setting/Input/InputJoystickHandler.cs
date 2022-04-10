using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public delegate void AxisHandler(float axis);

public class InputJoystickHandler : MonoBehaviour
{
    [SerializeField] 
    private Image _parentJoystick;

    [SerializeField]
    private RectTransform _joystick;

    [SerializeField]
    private Vector2 _startPosition;
    
    public event AxisHandler HorizontalHandler;

    public void OnTouchUpDown(bool isTouch)
    {
        if (isTouch)
        {
            TouchDown();
        }
        else
        {
            TouchUp();
        }
    }
    
    private void TouchDown()
    {
        Color color = _parentJoystick.color;
        color.a = 0;
        _parentJoystick.DOColor(color, 0.5f);
    }

    private void TouchUp()
    {
        Color color = _parentJoystick.color;
        color.a = 0.3f;
        _parentJoystick.DOColor(color, 0.5f);

        _joystick.position = _startPosition;
        HorizontalHandler?.Invoke(0.0f);
    }

    public void OnDragTouch(Vector2 eventData)
    {
        _joystick.anchoredPosition = new Vector2(eventData.x, _joystick.anchoredPosition.y);

        Vector2 currentPosition = _joystick.position;
        float direction = Mathf.Sign(currentPosition.x - _startPosition.x);
        HorizontalHandler?.Invoke(direction);
    }
}
