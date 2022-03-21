using UnityEngine;

public class BallStats : Stats
{
    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private Vector2 _direction;    

    public int Damage { get => _damage; set => _damage = value; }

    public Vector2 Direction { get => _direction; set => _direction = value; }
}
