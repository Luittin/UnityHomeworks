using System.Collections;
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

    public void StopShootting()
    {
        if (_shoot != null)
        {
            StopCoroutine(_shoot);
            _shoot = null;
        }
    }

    public void StartReload()
    {
        StopShootting();
        _reload = StartCoroutine(ReloadMagazine());
    }

    public void StopReload()
    {
        StopCoroutine(_reload);
        _reload = null;
        _gunStats.RechargeGun();
    }

    private IEnumerator Shoot()
    {
        while (true)
        {

            if (_gunStats.BulletInMagazine == 0)
            {
                _gunStats.RechargeGun();

                if (_gunStats.AllBullet == 0)
                {
                    yield return new WaitForSeconds(_shootDelay);
                }
            }

            _gunStats.BulletInMagazine--;

            CreateBollet(_gun.ObjectPool.GetObject());
            yield return new WaitForSeconds(_shootDelay);
        }
    }

    private void CreateBollet(Bullet bullet)
    {
        bullet.Damage = _gunStats.Damage;
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(transform.position + transform.forward);
    }


    private IEnumerator ReloadMagazine()
    {
        yield return new WaitForSeconds(_reloadDelay);
        StopReload();
    }
}
