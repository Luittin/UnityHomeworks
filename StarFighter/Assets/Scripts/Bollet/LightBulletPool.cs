using UnityEngine;

public class LightBulletPool : MonoBehaviour
{
    private ObjectPool<Bullet> _objectPool;

    [SerializeField]
    private GameObject _objectPrefab;

    private void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(CreateObject);
    }

    private Bullet CreateObject()
    {
        var createdBullet = Instantiate(_objectPrefab).GetComponent<Bullet>();
        return createdBullet;
    }

    public Bullet RequestObject()
    {
        return _objectPool.GetObject();
    }

    public void ReturnObjectToPool(Bullet anObject)
    {
        _objectPool.ReturnObject(anObject);
    }
}
