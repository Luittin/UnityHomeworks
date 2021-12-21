using System.Collections;
using UnityEngine;

public class GenerationEnemy : MonoBehaviour
{
    [SerializeField]
    private float _spaunDelay;
    [SerializeField, Range(0.0f, 50.0f)]
    private float _radiusSpawn = 20.0f;

    [SerializeField]
    private PoolManager _enemyManager;

    private Coroutine _generationEnemys;

    private void Start()
    {
        _generationEnemys = StartCoroutine(GenerationEnemys());
    }

    private IEnumerator GenerationEnemys()
    {
        while (true)
        {
            CreateEnemy();
            yield return new WaitForSecondsRealtime(_spaunDelay);
        }
    }

    private void CreateEnemy()
    {
        
        Enemy enemy = (Enemy)_enemyManager.RequestObject();
        Debug.Log(enemy);
        Vector3 spawnposition = Random.insideUnitSphere * _radiusSpawn;
        spawnposition.y = transform.position.y;
        enemy.transform.position = spawnposition;
        enemy.OnEndLifetime = () => { ReturnEnemyToPool(enemy); };
    }

    private void ReturnEnemyToPool(Enemy enemy)
    {
        _enemyManager.ReturnObjectToPool(enemy);
    }
}
