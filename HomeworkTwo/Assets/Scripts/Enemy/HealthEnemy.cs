using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class HealthEnemy : Health
{
    [SerializeField]
    private int _scorefordeath = 1;

    private Action<int> OnScoreUpdate;

    private void Awake()
    {
        OnScoreUpdate += GameManager.Instance.EnemyKilled;
    }

    protected override void CheckHealth()
    {
        if(Healths <= 0)
        {
            GetComponent<Enemy>().SleepEnemy();
            OnScoreUpdate?.Invoke(_scorefordeath);
        }
    }
}
