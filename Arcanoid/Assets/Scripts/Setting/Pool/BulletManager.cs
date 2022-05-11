using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private ObjectPool<BlockStats> _bulletPool;

    [SerializeField]
    private GameObject _bulletPrefab;

    private void Start()
    {
        _bulletPool = new ObjectPool<BlockStats>(CreateBullet);
    }

    private BlockStats CreateBullet()
    {
        var createdBullet = Instantiate(_bulletPrefab).GetComponent<BlockStats>();
        return createdBullet;
    }

    public BlockStats RequestBullet()
    {
        return _bulletPool.GetBullet();
    }

    public void ReturnBulletToPool(BlockStats bullet)
    {
        _bulletPool.ReturnBullet(bullet);
    }
}
