using UnityEngine;

public class ChangeSpeed : Effect
{
    [SerializeField]
    private float _changeSpeed = 1.0f;

    private float _originalSpeed;
    
    private void Awake()
    {
        _stopEffect += StopEffect;
    }
    
    public override void StartEffect()
    {
        ChangeSpeed parentChangeSize = _stats.GetComponentInChildren<ChangeSpeed>();
        if (parentChangeSize != null)
        {
            if (_changeSpeed == parentChangeSize._changeSpeed)
            {
                parentChangeSize._currentTime += _timeForEffect;
            }
            else
            {
                parentChangeSize._currentTime = -1f;
            }
            Destroy(gameObject);
        }

        _currentTime = _timeForEffect;
        _originalSpeed = _stats.Speed;
        _stats.Speed *= _changeSpeed;

        transform.parent = _stats.transform;
        _timerEffect = StartCoroutine(TimerEffect());
    }
    
    protected override void StopEffect()
    {
        _stats.Speed = _originalSpeed;
        _timerEffect = null;
        Destroy(gameObject);
    }
}
