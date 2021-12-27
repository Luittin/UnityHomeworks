using System.Collections;
using UnityEngine;

public class HealthPlayer : Health
{
    [SerializeField]
    private float _InvurabilityTime = 1.0f;

    private bool isShield = false;

    private Coroutine _timeToShield;

    private void Start()
    {
        GameManager.Instance.ChangeHealth(Healths);
    }

    protected override void CheckHealth()
    {
        if (!isShield)
        {
            GameManager.Instance.ChangeHealth(Healths);
            _timeToShield = StartCoroutine(TimeShield());
        }
    }

    private IEnumerator TimeShield()
    {
        yield return new WaitForSeconds(_InvurabilityTime);
        isShield = false;
    } 
}
