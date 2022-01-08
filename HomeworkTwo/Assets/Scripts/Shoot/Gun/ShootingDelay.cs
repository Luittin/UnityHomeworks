using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDelay : MonoBehaviour
{
    [SerializeField]
    private Gun _gun;
    [SerializeField]
    private GunStats _gunStats;
    [SerializeField]
    private float _shootDelay = 2.0f;
    [SerializeField]
    private float _reloadDelay = 3.0f;
    
    private Coroutine _shoot;
    private Coroutine _reload;

    public void Setup(Gun gun, GunStats gunStats)
    {
        _gun = gun;
        _gunStats = gunStats;
    }

    public void StartShooting()
    {
        if (_reload == null)
        {
            _shoot = StartCoroutine(Shoot());
        }
    }

    public void StopShotting()
    {
        if (_shoot != null)
        {
            StopCoroutine(_shoot);
            _shoot = null;
        }
    }

    public void StartReload()
    {
        StopShotting();
        _reload = StartCoroutine(ReloadMagazine());
    }

    public void StopReload()
    {
        StopCoroutine(_reload);
        _reload = null;
        _gunStats.RechargeGun();
        Debug.Log("SR");
    }

    private IEnumerator Shoot()
    {
        while (true)
        {

            if (_gunStats.BolletInMagazine == 0)
            {
                _gunStats.RechargeGun();

                if (_gunStats.AllBollet == 0)
                {
                    yield return new WaitForSeconds(_shootDelay);
                }
            }

            _gunStats.BolletInMagazine--;

            CreateBollet(_gun.ObjectPool.GetObject());
            yield return new WaitForSeconds(_shootDelay);
        }
    }

    private void CreateBollet(Bollet bollet)
    {
        bollet.Damage = _gunStats.Damage;
        bollet.transform.position = transform.position;
        bollet.transform.LookAt(transform.position + transform.forward);
    }


    private IEnumerator ReloadMagazine()
    {
        yield return new WaitForSeconds(_reloadDelay);
        StopReload();
    }
}
