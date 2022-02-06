using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public delegate void LoockingPatrole(bool isLooking);

public class BotMove : MonoBehaviour, IHandler
{
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private CheckPlayer _checkPlayer;

    [SerializeField]
    private Transform[] _targets;
    [SerializeField]
    private int _currentTargetIndex = 0;
    [SerializeField]
    private float _pausePatroleDelay = 2.0f;

    [SerializeField]
    public BehaviorTree _finalThee;

    private bool _isCamePosition = false;

    private Coroutine _lookingBot;

    public event AxisHandler OnHorizontalAxis;
    public event AxisHandler OnVerticalAxis;
    public event AxisHandler OnHorizontalMouseAxis;    
    public event AxisHandler OnVerticatMouseAxis;
    public event ButtonHandler OnJump;

    public event LoockingPatrole OnLooking;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        BehaviorTreeBuilder bt = new BehaviorTreeBuilder(gameObject)
            .Selector()
                    .Sequence()
                        .Condition(_checkPlayer.CheckForPlayer)
                            .Sequence()
                                .Do("Follow Player", () => { PursuitPlayer(); return TaskStatus.Success; })
                            .End()
                    .End()
                    .Sequence()
                        .Sequence()
                            .Condition("Reached The Point", () => { return _isCamePosition; })
                                .Sequence()
                                    .Do("Pause", () => { StartLooking(); return TaskStatus.Success; })
                                .End()
                        .End()
                    .End()
                    .Sequence()
                        .Do("Patrole", () => { BotPatrole(); return TaskStatus.Success; })
                    .End()
            .End();

        _finalThee = bt.Build();
    }

    private void Update()
    {
        _finalThee.Tick();
    }

    private void NewTargetPatrole()
    {
        _currentTargetIndex = _currentTargetIndex + 1 < _targets.Length ? _currentTargetIndex + 1 : 0;
    }

    private void BotPatrole()
    {
        _agent.SetDestination(_targets[_currentTargetIndex].position);
        OnVerticalAxis?.Invoke(1.0f);
        CameThePosition();
    }

    private void StartLooking()
    {
        if (_lookingBot == null)
        {
            OnVerticalAxis?.Invoke(0.0f);
            _lookingBot = StartCoroutine(Looking());
        }
    }

    private void StopLooking()
    {
        if(_lookingBot != null)
        {
            StopCoroutine(_lookingBot);
            _lookingBot = null;
        }
        _isCamePosition = false;
    }

    private IEnumerator Looking()
    {
        OnLooking?.Invoke(true);
        yield return new WaitForSeconds(_pausePatroleDelay);
        OnLooking?.Invoke(false);
        StopLooking();
    }

    private void CameThePosition()
    {
        if(!_agent.pathPending && !_agent.hasPath)
        {
            _isCamePosition = true;
            NewTargetPatrole();
        }
        else
        {
            _isCamePosition = false;
        }
    }

    private void PursuitPlayer()
    {
        _agent.SetDestination(_checkPlayer.PlayerTransform.position);
        OnVerticalAxis?.Invoke(1.0f);
    }
}
