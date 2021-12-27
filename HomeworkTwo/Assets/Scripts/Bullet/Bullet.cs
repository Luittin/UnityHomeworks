using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField, Range(0.0f, 100.0f)]
    private float _speed = 50.0f;
    [SerializeField, Range(0.0f, 20.0f)]
    private float _lifeTime = 5.0f;
    
    private int _damageValue = 1;

    private Rigidbody _rigidbody;

    private Action onEndLifetime;
    private Coroutine _lifetimeCoroutine;
    
    public int DamageValue { set => _damageValue = value; }
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
        _rigidbody.velocity = Vector3.zero;
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
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().Healths -= _damageValue;
        }
        SleepObject();
    }
}
