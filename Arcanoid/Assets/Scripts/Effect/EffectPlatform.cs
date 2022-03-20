using UnityEngine;

public class EffectPlatform : GameObject
{
    [SerializeField]
    private float _changeSpeed = 2f;

    [SerializeField]
    private PlatformStats _ballStats;

    private void Awake()
    {
        _ballStats = GetComponent<PlatformStats>();
        StartEffect();
    }

    private void StartEffect()
    {
        _ballStats.Speed *= _changeSpeed;
        _coroutine = StartCoroutine(TimeForEffect());
    }
}
