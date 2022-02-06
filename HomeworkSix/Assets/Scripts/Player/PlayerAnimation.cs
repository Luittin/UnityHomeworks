using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private IHandler _inputHandler;

    [SerializeField]
    private string _nameJampAnimation = "BasicMotions@Jump01";

    private Animator _animator;

    private static string MOVE_AXIS_X = "MoveAxis_X";
    private static string MOVE_AXIS_Y = "MoveAxis_Y";
    private static string JUMP = "Jump";

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _inputHandler = GetComponent<IHandler>();

        _inputHandler.OnHorizontalAxis += OnHorizontalAnimation;
        _inputHandler.OnVerticalAxis += OnVerticalAnimation;
        _inputHandler.OnJump += OnJumpAnimation;
    }

    public void OnHorizontalAnimation(float axisValue)
    {
        _animator.SetFloat(MOVE_AXIS_X, axisValue);
    }

    public void OnVerticalAnimation(float axisValue)
    {
        _animator.SetFloat(MOVE_AXIS_Y, axisValue);
    }

    public void OnJumpAnimation(ButtonState buttonState)
    {
        if(buttonState == ButtonState.PressDown && _animator.GetCurrentAnimatorStateInfo(0).IsName(_nameJampAnimation) == false)
        {
            _animator.SetTrigger(JUMP);
        }
    } 
}
