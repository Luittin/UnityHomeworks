using UnityEngine;

public class PlaerMove : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private Vector2 _directionJump;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputManager.OnJumpPress += JumpPlayer;
    }

    private void JumpPlayer()
    {
        _rigidbody2D.AddForce(_directionJump);
    }
}
