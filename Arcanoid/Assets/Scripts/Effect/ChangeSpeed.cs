using UnityEngine;

public class ChangeSpeed : Effect
{
    [SerializeField]
    private float _changeSpeed = 1.0f;

    protected virtual void StartEffect()
    {
        _stats.Speed *= _changeSpeed;
    }
}
