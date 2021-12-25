using System.Collections;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField]
    private int _health = 10;

    [SerializeField]
    private float _shieldTime = 1.0f;

    public int Health { get => _health; set => ChangeHealth(value); }

    private bool isShield = false;

    private Coroutine _timeToShield;

    private void Start()
    {
        GameManager.Instance.ChangeHealth(_health);
    }

    private void ChangeHealth(int health)
    {
        if (!isShield)
        {
            _health = health;
            GameManager.Instance.ChangeHealth(_health);
            _timeToShield = StartCoroutine(TimeShield());
        }
    }

    private IEnumerator TimeShield()
    {
        yield return new WaitForSeconds(_shieldTime);
        isShield = false;
    } 
}
