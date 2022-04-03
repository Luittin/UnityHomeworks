using UnityEngine;

public class ChangeSize : Effect
{
    [SerializeField]
    private float _changeSize = 1.0f;

    public override void StartEffect()
    {
        _stats.Size *= _changeSize;
    }
}
