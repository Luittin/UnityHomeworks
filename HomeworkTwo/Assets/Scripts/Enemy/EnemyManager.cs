using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private ObjectPool<Enemy> _enemyPool;

    [SerializeField]
    private GameObject _enemyPrefab;

    private void Start()
    {
        _enemyPool = new ObjectPool<Enemy>(CreateEnemy);
    }

    private Enemy CreateEnemy()
    {
        var createdEnemy = Instantiate(_enemyPrefab).GetComponent<Enemy>();
        return createdEnemy;
    }

    public Enemy RequestEnemy()
    {
        return _enemyPool.GetObject();
    }

    public void ReturnEnemyToPool(Enemy enemy)
    {
        _enemyPool.ReturnObject(enemy);
    }
}
