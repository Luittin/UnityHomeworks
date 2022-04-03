using UnityEngine;

public class ChangeSpeed : Effect
{
    [SerializeField]
    private float _changeSpeed = 1.0f;

    public override void StartEffect()
    {
        _stats.Speed *= _changeSpeed;
    }
}
