using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private ObjectPool<Enemy> _objectPool;

    [SerializeField]
    private GameObject _objectPrefab;

    private void Awake()
    {
        _objectPool = new ObjectPool<Enemy>(CreateObject);
    }

    private Enemy CreateObject()
    {
        var createdBullet = Instantiate(_objectPrefab).GetComponent<Enemy>();
        return createdBullet;
    }

    public Enemy RequestObject()
    {
        return _objectPool.GetObject();
    }

    public void ReturnObjectToPool(Enemy anObject)
    {
        _objectPool.ReturnObject(anObject);
    }
}
