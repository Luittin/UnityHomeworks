using System;
using System.Collections;
using UnityEngine;

public class HealthPlayer : Health
{
    [SerializeField]
    private float _InvurabilityTime = 1.0f;

    private bool isShielded = false;

    private Coroutine _timeToShield;

    private Action<int> OnHealthHandler;

    private void Start()
    {
        OnHealthHandler += GameManager.Instance.ChangeHealth;
        OnHealthHandler?.Invoke(Healths);
    }

    protected override void CheckHealth()
    {
        if (!isShielded)
        {
            OnHealthHandler?.Invoke(Healths);
            _timeToShield = StartCoroutine(TimeShield());
        }
    }

    private IEnumerator TimeShield()
    {
        yield return new WaitForSeconds(_InvurabilityTime);
        isShielded = false;
    } 
}
