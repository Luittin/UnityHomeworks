using UnityEngine;

public class UIMainMenuController : MonoBehaviour, IMenuController
{
    [SerializeField]
    private State _startMenu;

    private State _nextState;
    private State _currentState;

    private void Awake()
    {
        Initialize(_startMenu);
    }

    public void OnSelectMenu(State nextState)
    {
        ChangeState(nextState);        
    }

    public void Initialize(State startingState)
    {
        _currentState = startingState;
        _currentState.Enter();
        _currentState.AddListener(StartNewState);
    }

    public void ChangeState(State newState)
    {
        _nextState = newState;
        StopCurrentState();
    }

    public void StopCurrentState()
    {
        _currentState.Exit();
        
    }

    public void StartNewState()
    {
        _currentState = _nextState;
        _currentState.AddListener(StartNewState);
        _nextState.Enter();
    }
}
