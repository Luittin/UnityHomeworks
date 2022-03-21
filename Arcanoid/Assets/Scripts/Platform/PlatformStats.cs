using UnityEngine;

public class PlatformStats : Stats
{
    [SerializeField]
    private float _trafficLimiter = 1.4f;
    public float TrafficLimiter { get => _trafficLimiter; set => _trafficLimiter = value; }
}
