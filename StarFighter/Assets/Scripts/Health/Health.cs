using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _healths = 100;

    public int Healths { get => _healths; set => ChangeHealth(value); }

    private void ChangeHealth(int health)
    {
        _healths = health;
        CheckHealth();
    }

    protected virtual void CheckHealth()
    {
        throw new NotImplementedException();
    }
}
