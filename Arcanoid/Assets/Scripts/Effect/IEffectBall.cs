using UnityEngine;

public class EffectBall : GameObject
{
    [SerializeField]
    private float _changeSpeed = 2f;

    [SerializeField]
    private BallStats _ballStats;

    private void Awake()
    {
        _ballStats = GetComponent<BallStats>();
        StartEffect();
    }

    private void StartEffect()
    {
        _ballStats.Speed *= _changeSpeed;
        _coroutine = StartCoroutine(TimeForEffect());
    }
}
