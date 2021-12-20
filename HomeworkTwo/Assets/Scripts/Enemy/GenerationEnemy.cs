using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private float _spaunDelay;
    [SerializeField, Range(0.0f, 50.0f)]
    private float _radiusSpaun = 20.0f;

    private EnemyManager _enemyManager;

    private Coroutine generationEnemys;

    private void Start()
    {
        _enemyManager = FindObjectOfType<EnemyManager>();
        if (_enemyManager == null)
        {
            Debug.Log("Bullet Manager not found!");
        }
        generationEnemys = StartCoroutine(GenerationEnemys());
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
        
        Enemy enemy = _enemyManager.RequestEnemy();
        Debug.Log(enemy);
        Vector3 spawnposition = Random.insideUnitSphere * _radiusSpaun;
        spawnposition.y = transform.position.y;
        enemy.transform.position = spawnposition;
        enemy.OnEndLifetime = () => { ReturnEnemyToPool(enemy); };
    }

    private void ReturnEnemyToPool(Enemy enemy)
    {
        _enemyManager.ReturnEnemyToPool(enemy);
    }
}
