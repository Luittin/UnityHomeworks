using System;
using System.Collections;
using UnityEngine;

public class Blocks : MonoBehaviour, IPoolable
{
    [SerializeField]
    private Coins _coins;

    [SerializeField]
    private float _lifeTime = 5.0f;

    private Coroutine _lifeTimeCoroutine;

    private Action onEndLifetime;

    public Action OnEndLifetime { set => onEndLifetime = value; }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        SleepEnemy();
    }

    public void SleepEnemy()
    {
        onEndLifetime.Invoke();
    }

    public void RequestFromPool()
    {
        gameObject.SetActive(true);
        _coins.transform.localPosition = Vector2.zero;
        _coins.gameObject.SetActive(true);
        _lifeTimeCoroutine = StartCoroutine(LifeTime());
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        if (_lifeTimeCoroutine != null)
        {
            StopCoroutine(_lifeTimeCoroutine);
            _lifeTimeCoroutine = null;
        }
    }
}
