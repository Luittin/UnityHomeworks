using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private float _shootingDelay;
    [SerializeField, Range(0.0f, 10.0f)]
    private float _radiusHardShooting = 0.5f;

    [SerializeField]
    private InputPlayer _inputPlayer;

    private BulletManager _bulletManager;

    private Coroutine _lightShootingCoroutine;
    private Coroutine _hardShootingCoroutine;

    private void Start()
    {
        _inputPlayer = FindObjectOfType<InputPlayer>();
        _bulletManager = FindObjectOfType<BulletManager>();
        if (_bulletManager == null)
        {
            Debug.Log("Bullet Manager not found!");
        }
    }

    private void Update()
    {
        if (_inputPlayer.LightShooting)
        {
            StartLightShooting();
        }
        else
        {
            StopLightShooting();
        }
        if (_inputPlayer.HardShooting)
        {
            StartHardShooting();
        }
    }

    private void StartHardShooting()
    {
        if(_hardShootingCoroutine == null)
        {
            _hardShootingCoroutine = StartCoroutine(HardShootingCoroutine());
        }
    }

    private IEnumerator HardShootingCoroutine()
    {
        while (true)
        {
            HardShoot();
            yield return new WaitForSeconds(_shootingDelay);
        }
    }

    private void StopHardSooting()
    {
        if (_hardShootingCoroutine != null)
        {
            StopCoroutine(_hardShootingCoroutine);
            _hardShootingCoroutine = null;
        }
    }

    private void StartLightShooting()
    {
        if (_lightShootingCoroutine == null)
        {
            _lightShootingCoroutine = StartCoroutine(LightShootCoroutine());
        }
    }

    private void HardShoot()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, _radiusHardShooting, transform.forward);
        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>().SleepEnemy();
            }
        }
    }

    public void StopLightShooting()
    {
        if (_lightShootingCoroutine != null)
        {
            StopCoroutine(_lightShootingCoroutine);
            _lightShootingCoroutine = null;
        }
    }

    private IEnumerator LightShootCoroutine()
    {
        while (true)
        {            
            LightShoot();
            yield return new WaitForSeconds(_shootingDelay);
        }
    }

    private void LightShoot()
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
