using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public delegate void AxisHandler(float axis);

[RequireComponent(typeof(Image))]
public class InputJoystickHandler : MonoBehaviour
{
    [SerializeField] 
    private Image _arrowLeft;
    [SerializeField]
    private Image _arrowRight;

    [SerializeField]
    private RectTransform _transform;
    [SerializeField]
    private Image _image;   

    [SerializeField]
    private Vector2 _startPosition;

    [SerializeField]
    private float _minAlfa = 0.0f;
    [SerializeField]
    private float _maxAlfa = 0.3f;
    [SerializeField]
    private float _timeEffect = 0.5f;

    private bool isActive = false;

    public event AxisHandler HorizontalHandler;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void OnTouchUpDown(bool isTouch)
    {
        if (isActive)
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
    }
    
    private void TouchDown()
    {
        HideArrow();
        SetAlfaImageElement(_image, _maxAlfa);
    }

    private void TouchUp()
    {
        HideArrow();

        SetAlfaImageElement(_image, _minAlfa);

        _transform.position = _startPosition;
        HorizontalHandler?.Invoke(0.0f);
    }

    public void OnDragTouch(Vector2 eventData)
    {
        if (isActive)
        {
            _transform.position = new Vector2(eventData.x, eventData.y);

            Vector2 currentPosition = _transform.position;
            float direction = Mathf.Sign(currentPosition.x - _startPosition.x);

            ShowArrow(direction);

            HorizontalHandler?.Invoke(direction);
        }
    }

    private void HideArrow()
    {
        SetAlfaImageElement(_arrowLeft, _minAlfa);
        SetAlfaImageElement(_arrowRight, _minAlfa);
    }

    public void SetAlfaImageElement(Image image, float alfa)
    {
        Color color = image.color;
        color.a = alfa;
        image.DOColor(color, _timeEffect);
    }

    private void ShowArrow(float direction)
    {
        if(direction < 0)
        {
            SetAlfaImageElement(_arrowLeft, _maxAlfa);
            SetAlfaImageElement(_arrowRight, _minAlfa);
        }
        else if(direction > 0)
        {
            SetAlfaImageElement(_arrowLeft, _minAlfa);
            SetAlfaImageElement(_arrowRight, _maxAlfa);
        }
        else
        {
            SetAlfaImageElement(_arrowLeft, _minAlfa);
            SetAlfaImageElement(_arrowRight, _minAlfa);
        }
        
    }
    public void DisableHandler()
    {
        gameObject.SetActive(false);
        HorizontalHandler?.Invoke(0.0f);
        HideArrow();
        SetAlfaImageElement(_image, _minAlfa);
        isActive = false;
    }

    public void AnableHandler()
    {
        gameObject.SetActive(true);
        isActive = true;
    }
}
