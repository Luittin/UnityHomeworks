using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField, Range(0.0f, 10.0f)]
    private float _speed = 2.0f;
    [SerializeField, Range(0.0f,20.0f)]
    private float _lifeTime = 5.0f;

    private Action onEndLifetime;
    private Coroutine _lifetimeCoroutine;

    public Action OnEndLifetime { set => onEndLifetime = value; }

    private void Update()
    {
        Vector3 moveVector = transform.forward * _speed * Time.deltaTime;
        transform.position += moveVector.normalized;
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        SleepObject();
    }

    private void SleepObject()
    {
        gameObject.SetActive(false);
        onEndLifetime.Invoke();
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }
    }

    public void RequestFromPool()
    {
        gameObject.SetActive(true);
        _lifetimeCoroutine = StartCoroutine(LifeTime());
    }
}
