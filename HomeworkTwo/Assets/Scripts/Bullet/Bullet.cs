using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField, Range(0.0f, 10.0f)]
    private float _speed = 2.0f;
    [SerializeField, Range(0.0f, 20.0f)]
    private float _lifeTime = 5.0f;

    private Rigidbody _rigidbody;

    private Action onEndLifetime;
    private Coroutine _lifetimeCoroutine;

    public Action OnEndLifetime { set => onEndLifetime = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveForce = transform.forward * _speed * Time.fixedDeltaTime;
        _rigidbody.AddForce(moveForce, ForceMode.Force);
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

    private void OnCollisionEnter(Collision collision)
    {
        SleepObject();
    }
}
