using System;
using System.Collections;
using UnityEngine;

public class IGun : MonoBehaviour
{
    [SerializeField]
    private float _shootingDelay;

    private Coroutine _ShootingCoroutine;

    public void StartShooting()
    {
        if (_ShootingCoroutine == null)
        {
            _ShootingCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_shootingDelay);
        }
    }

    protected virtual void Shoot()
    {
        throw new NotImplementedException();
    }

    public void StopSooting()
    {
        if (_ShootingCoroutine != null)
        {
            StopCoroutine(_ShootingCoroutine);
            _ShootingCoroutine = null;
        }
    }
}
