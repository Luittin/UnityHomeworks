using UnityEngine;

public class Effeñt : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private string _nameEffect = "Speed";
    [SerializeField]
    private float _speedEffect = 0.5f;
    [SerializeField]
    private float _timeEffect = 2.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            
        }
    }
}
