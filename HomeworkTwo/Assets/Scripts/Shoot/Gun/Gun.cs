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
    private Bollet _bollet;

    private ObjectPool<Bollet> _objectPool;

    public ObjectPool<Bollet> ObjectPool { get => _objectPool; }
    public ShootingDelay ShootingDelay { get => _shootingDelay; }
    public Sprite Icon { get => _icon; }
    public GunStats GunStats { get => _gunStats; }

    private void Awake()
    {
        _objectPool = new ObjectPool<Bollet>(CreateObject);
        _shootingDelay.Setup(this, _gunStats);
        _gunStats.Setup(_shootingDelay);
    }

    private Bollet CreateObject()
    {
        var createdBullet = Instantiate(_bollet).GetComponent<Bollet>();
        createdBullet.OnEndLifetime += ReturnObjectToPool;
        return createdBullet;
    }

    public void ReturnObjectToPool(Bollet anObject)
    {
        _objectPool.ReturnObject(anObject);
    }
}
