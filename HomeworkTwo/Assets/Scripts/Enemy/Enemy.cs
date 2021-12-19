using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField, Range(0.0f, 10.0f)]
    private float _speed = 2.0f;

    [SerializeField]
    private Transform target;

    private Action onEndLifetime;
    public Action OnEndLifetime { set => onEndLifetime = value; }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {

        }
        if(collision.gameObject.name == "Bullet")
        {
            SleepEnemy();
        }
    }

    public void SleepEnemy()
    {
        onEndLifetime.Invoke();
    }

    public void RequestFromPool()
    {
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
