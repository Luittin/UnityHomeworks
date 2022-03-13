using UnityEngine;

public class BallStats : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private Vector2 _direction;    

    public int Damage { get => _damage; set => _damage = value; }
    public float Speed { get => _speed; set => _speed = value; }

    public Vector2 Direction { get => _direction; set => _direction = value; }
}
