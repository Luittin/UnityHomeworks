using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private ObjectPool<IPoolable> _objectPool;

    [SerializeField]
    private GameObject _objectPrefab;

    private void Start()
    {
        _objectPool = new ObjectPool<IPoolable>(CreateObject);
    }

    private IPoolable CreateObject()
    {
        var createdBullet = Instantiate(_objectPrefab).GetComponent<IPoolable>();
        return createdBullet;
    }

    public IPoolable RequestObject()
    {
        return _objectPool.GetObject();
    }

    public void ReturnObjectToPool(IPoolable anObject)
    {
        _objectPool.ReturnObject(anObject);
    }
}
