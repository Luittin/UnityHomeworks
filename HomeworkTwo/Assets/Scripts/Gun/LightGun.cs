using UnityEngine;

public class LightGun : Gun
{
    [SerializeField]
    private PoolManager _bulletManager;

    protected override void Shoot()
    {
        Bullet bullet = (Bullet)_bulletManager.RequestObject();
        bullet.DamageValue = DamageValue;
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(transform.position + transform.forward);
        bullet.OnEndLifetime = () => { ReturnBulletToPool(bullet); };
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        _bulletManager.ReturnObjectToPool(bullet);
    }
}
