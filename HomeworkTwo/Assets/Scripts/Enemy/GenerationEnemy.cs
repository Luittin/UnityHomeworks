using System.Collections;
using UnityEngine;

public class GenerationEnemy : MonoBehaviour
{
    [SerializeField]
    private float _spawnDelay;
    [SerializeField, Range(0.0f, 50.0f)]
    private float _radiusSpawn = 20.0f;

    [SerializeField]
    private EnemyManager _enemyManager;

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
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }

    private void CreateEnemy()
    {

        Enemy enemy = _enemyManager.RequestObject();
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
