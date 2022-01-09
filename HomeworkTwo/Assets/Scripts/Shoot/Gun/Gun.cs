using UnityEngine;
[RequireComponent(typeof(ShootingDelay), typeof(GunStats))]
public class Gun : MonoBehaviour
{
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private ShootingDelay _shootingDelay;
    [SerializeField]
    private GunStats _gunStats;
    [SerializeField]
    private Bullet _bullet;

    private ObjectPool<Bullet> _objectPool;

    public ObjectPool<Bullet> ObjectPool { get => _objectPool; }
    public ShootingDelay ShootingDelay { get => _shootingDelay; }
    public Sprite Icon { get => _icon; }
    public GunStats GunStats { get => _gunStats; }

    private void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(CreateObject);
        _shootingDelay.Setup(this, _gunStats);
        _gunStats.Setup(_shootingDelay);
    }

    private Bullet CreateObject()
    {
        var createdBullet = Instantiate(_bullet).GetComponent<Bullet>();
        createdBullet.OnEndLifetime += ReturnObjectToPool;
        return createdBullet;
    }

    public void ReturnObjectToPool(Bullet anObject)
    {
        _objectPool.ReturnObject(anObject);
    }
}
