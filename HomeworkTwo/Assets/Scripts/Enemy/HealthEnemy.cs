using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class HealthEnemy : Health
{
    [SerializeField]
    private int _scorefordeath = 1;

    protected override void CheckHealth()
    {
        if(Healths <= 0)
        {
            GetComponent<Enemy>().SleepEnemy();
            GameManager.Instance.EnemyKilled(_scorefordeath);
        }
    }
}
