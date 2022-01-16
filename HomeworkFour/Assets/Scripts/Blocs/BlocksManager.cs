using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    private ObjectPool<Blocks> _objectPool;

    [SerializeField]
    private GameObject _objectPrefab;

    private void Awake()
    {
        _objectPool = new ObjectPool<Blocks>(CreateObject);
    }

    private Blocks CreateObject()
    {
        var createdBullet = Instantiate(_objectPrefab).GetComponent<Blocks>();
        return createdBullet;
    }

    public Blocks RequestObject()
    {
        return _objectPool.GetObject();
    }

    public void ReturnObjectToPool(Blocks anObject)
    {
        _objectPool.ReturnObject(anObject);
    }
}
