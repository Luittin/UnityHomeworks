using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float _shootingDelay;

    [SerializeField]
    private int _damageValue = 1;

    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private int _booletMagazine = 10;
    [SerializeField]
    private int _maxBulletMagazine = 10;
    [SerializeField]
    private int _allBullet = 100;

    private Coroutine _ShootingCoroutine;

    public Sprite Icon { get => _icon; set => _icon = value; }
    public int BooletMagazine { get => _booletMagazine; set => _booletMagazine = value; }
    public int AllBullet { get => _allBullet; set => _allBullet = value; }
    public int MaxBulletMagazine { get => _maxBulletMagazine; set => _maxBulletMagazine = value; }
    public int DamageValue { get => _damageValue; set => _damageValue = value; }

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
            if(_booletMagazine == 0)
            {
                RechargeGun();

                if(_allBullet == 0)
                {
                    yield return new WaitForSeconds(_shootingDelay);
                }
            }

            Shoot();

            _booletMagazine--;

            GameManager.Instance.RefrashGunMenu(_booletMagazine, _allBullet);

            yield return new WaitForSeconds(_shootingDelay);
        }
    }

    protected virtual void Shoot()
    {
        throw new NotImplementedException();
    }

    public void RechargeGun()
    {
        if(_booletMagazine == _maxBulletMagazine)
        {
            return;
        }
        if(_booletMagazine != 0)
        {
            _allBullet += _booletMagazine;
            _booletMagazine = 0;
        }
        if (_allBullet >= _maxBulletMagazine)
        {
            _booletMagazine = _maxBulletMagazine;
            _allBullet -= _maxBulletMagazine;
        }
        else if (_allBullet > 0)
        {
            _booletMagazine = _allBullet;
            _allBullet = 0;
        }
        GameManager.Instance.RefrashGunMenu(_booletMagazine, _allBullet);
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
