using System;
using UnityEngine;

public class MenuEffect : MonoBehaviour
{
    public event Action EndDisappearingEffect;

    public virtual void AppearanceEffect()
    {

    }

    public virtual void DisappearingEffect()
    {
        EndDisappearingEffect?.Invoke();
    }
}
