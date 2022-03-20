using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject : MonoBehaviour
{
    [SerializeField]
    protected float _time = 5.0f;
    [SerializeField]
    protected float _timeSpeed = 0.1f;

    protected Coroutine _coroutine;

    protected IEnumerator TimeForEffect()
    {
        while (_time >= 0.0f)
        {
            yield return new WaitForSeconds(_timeSpeed);
            _time -= _timeSpeed;
        }

    }

    protected virtual void StopEffect()
    {
        StopCoroutine(_coroutine);
        _coroutine = null;
        Destroy(this);
    }
}
