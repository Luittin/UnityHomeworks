using System.Collections;
using UnityEngine;

public class PhisicsBullet : Bullet
{
    [SerializeField]
    private float _moveSpeed = 2.0f;
    [SerializeField]
    private float _lifeTime = 5.0f;
    [SerializeField]
    private Rigidbody _rigidbody;

    private Coroutine _coroutineLifeTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveForce = transform.forward * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody.AddForce(moveForce, ForceMode.Force);
    }

    private IEnumerator BulletLefeTime()
    {
        yield return new WaitForSecondsRealtime(_lifeTime);
        SleepObject();
    }

    private void SleepObject()
    {
        _rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
        onEndLifetime.Invoke(this);
    }

    public override void ReturnToPool()
    {
        gameObject.SetActive(false);
        if (_coroutineLifeTime != null)
        {
            StopCoroutine(_coroutineLifeTime);
            _coroutineLifeTime = null;
        }
    }

    public override void RequestFromPool()
    {
        gameObject.SetActive(true);
        _coroutineLifeTime = StartCoroutine(BulletLefeTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthEnemy>().Healths -= Damage;            
        }
        SleepObject();
    }
}
