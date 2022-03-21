using UnityEngine;

public class ChangeSize : Effect
{
    [SerializeField]
    private float _changeSize = 1.0f;

    protected virtual void StartEffect()
    {
        _stats.Size *= _changeSize;
    }
}
