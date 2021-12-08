using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private ObjectPool<Bullet> _bulletPool;

    [SerializeField]
    private GameObject _bulletPrefab;

    private void Start()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet);
    }

    private Bullet CreateBullet()
    {
        var createdBullet = Instantiate(_bulletPrefab).GetComponent<Bullet>();
        return createdBullet;
    }

    public Bullet RequestBullet()
    {
        return _bulletPool.GetBullet();
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        _bulletPool.ReturnBullet(bullet);
    }
}
