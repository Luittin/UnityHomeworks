using System;
using UnityEngine;

public class ChangeSize : Effect
{
    [SerializeField]
    private float _changeSize = 1.0f;

    private float _originalSize;
    
    private void Awake()
    {
        _stopEffect += StopEffect;
    }

    public override void StartEffect()
    {
        ChangeSize parentChangeSize = _stats.GetComponentInChildren<ChangeSize>();
        if (parentChangeSize != null)
        {
            if (_changeSize == parentChangeSize._changeSize)
            {
                parentChangeSize._currentTime += _timeForEffect;
            }
            else
            {
                parentChangeSize._currentTime = -1f;
            }
        }

        _originalSize = _stats.Size;
        _stats.Size *= _changeSize;
        transform.parent = _stats.transform;
        _timerEffect = StartCoroutine(TimerEffect());
    }

    protected override void StopEffect()
    {
        _stats.Size = _originalSize;
        _timerEffect = null;
        Destroy(this.gameObject);
    }
}
