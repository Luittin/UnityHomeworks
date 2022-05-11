using UnityEngine;
using UnityEngine.EventSystems;

public delegate void PositionHandler(Vector2 eventPosition);

public delegate void TouchHandler(bool isTouch);
public class InputTouchHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public event PositionHandler HorizontalHandler;
    public event TouchHandler TouchUpOrDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchUpOrDown?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchUpOrDown?.Invoke(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HorizontalHandler?.Invoke(eventData.position);
    }
}
