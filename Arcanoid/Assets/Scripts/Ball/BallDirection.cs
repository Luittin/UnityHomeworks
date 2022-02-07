using UnityEngine;

class BallDirection : MonoBehaviour
{
    [SerializeField]
    private Vector3 _direction;

    public Vector3 Direction { get => _direction; set => _direction = value; }
}
