using UnityEngine;

public class PlatformStats : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private float _trafficLimiter = 1.4f;

    public float Speed { get => _speed; set => _speed = value; }
    public float TrafficLimiter { get => _trafficLimiter; set => _trafficLimiter = value; }
}
