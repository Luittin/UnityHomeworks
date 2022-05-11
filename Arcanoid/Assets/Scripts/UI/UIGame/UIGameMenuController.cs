using System;
using UnityEngine;

public class UIGameMenuController : MonoBehaviour, IMenuController
{
    public event Action WentPause;
    public event Action ComOutPause;
    public event Action ReloadGame;
    public event Action ClickOnSound;
    public event Action ClickOnHome;
    public event Action NextLevel;

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

    private void Initialize(State startingState)
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

    public void OnChangeLife(int currentLife)
    {
        
    }

    private void OnEndGame(int currentLife)
    {
        
    }

    public void OnChangeTargetInput()
    {

    }
}
