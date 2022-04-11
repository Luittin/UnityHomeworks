using System;
using UnityEngine;

public class BallSight : MonoBehaviour
{
    [SerializeField]
    private float _minDegree = -170.0f;
    [SerializeField]
    private float _maxDegree = -10.0f;

    private bool isInput = false;

    private Vector2 _direction = Vector2.zero;
    
    public Action<Vector2> DirectionSight;

    public void OnDrag(Vector2 eventPosition)
    {
        if (isInput)
        {
            float x = transform.position.x - eventPosition.x;
            float y = -Mathf.Abs(transform.position.y - eventPosition.y);

            float grad = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            grad = Mathf.Clamp(grad, _minDegree, _maxDegree);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, grad + 90));
            _direction = new Vector2(-x, -y).normalized;
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
