using System;
using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    protected Stats _stats;

    [SerializeField]
    protected float _timeForEffect = 5.0f;
    [SerializeField]
    protected float _currentTime = 0.0f;

    protected Coroutine _timerEffect;

    protected Action _stopEffect;

    public Stats Stats { get => _stats; set => _stats = value; }
    
    public virtual void StartEffect()
    {
        
    }
    
    protected IEnumerator TimerEffect()
    {
        while(_currentTime >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            _currentTime -= 0.1f;
        }

        _stopEffect?.Invoke();
    }
}
