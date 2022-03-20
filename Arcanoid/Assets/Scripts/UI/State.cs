using System;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField]
    private MenuEffect _menuEffect;

    public Action ExitStateDone;

    private void Awake()
    {
        if (_menuEffect != null)
        {
            _menuEffect.EndDisappearingEffect += OnEndExitEffect;
        }
    }

    public void Enter()
    {
        gameObject.SetActive(true);
        if (_menuEffect != null)
        {
            _menuEffect.AppearanceEffect();
        }
    }

    public void Exit()
    {
        if(_menuEffect != null)
        {
            _menuEffect.DisappearingEffect();
        }
        else
        {
            OnEndExitEffect();
        }
    }

    public void OnEndExitEffect()
    {
        gameObject.SetActive(false);
        ExitStateDone?.Invoke();
    }
}
