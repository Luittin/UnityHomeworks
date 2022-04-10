using System;
using UnityEngine;

public class BallSight : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;
    
    private bool isInput = false;

    private Vector2 _direction = Vector2.zero;
    
    public Action<Vector2> DirectionSight;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(Vector2 eventPosition)
    {
        if (isInput)
        {
            float a = transform.position.x - eventPosition.x;
            float b = -Mathf.Abs(transform.position.y - eventPosition.y);

            float grad = Mathf.Atan2(b, a) * Mathf.Rad2Deg;
            grad = Mathf.Clamp(grad, -170, -10);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, grad + 90));
            _direction = new Vector2(-a, -b).normalized;
            transform.rotation = rotation;
        }
    }

    public void OnTouchUpDown(bool isTouch)
    {
        isInput = isTouch;

        if (!isInput)
        {
            DirectionSight?.Invoke(_direction);
        }
    }
}
