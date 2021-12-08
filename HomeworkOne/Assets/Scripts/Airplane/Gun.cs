using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float _shootingDelay;

    [SerializeField]
    private PlayerInputHandler _playerInputHandler;

    private BulletManager _bulletManager;

    private Coroutine _shootingCoroutine;

    private void Start()
    {
        _bulletManager = FindObjectOfType<BulletManager>();
        if (_bulletManager == null)
        {
            Debug.Log("Bullet Manager not found!");
        }
    }

    private void Update()
    {
        if (_playerInputHandler.Fire) StartShooting();
        else StopShooting();

        Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.red);
    }

    public void StartShooting()
    {
        if(_shootingCoroutine == null)
            _shootingCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShooting()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;
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

    private void Shoot()
    {
        Bullet bullet = _bulletManager.RequestBullet();
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(transform.position + transform.forward * 10.0f);
        bullet.OnEndLifetime = () => { ReturnBulletToPool(bullet); };
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        _bulletManager.ReturnBulletToPool(bullet);
    }
}
