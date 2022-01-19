using UnityEngine;
using UnityEngine.AI;

public class BotMove : MonoBehaviour, IHandler
{
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private Transform _target;

    public event AxisHandler OnHorizontalAxis;
    public event AxisHandler OnVerticalAxis;
    public event AxisHandler OnHorizontalMouseAxis;    
    public event AxisHandler OnVerticatMouseAxis;
    public event ButtonHandler OnJump;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    private void Update()
    {
        MoveEnemy();       
    }

    private void MoveEnemy()
    {
        if (_agent.remainingDistance == 0.0f)
        {
            _agent.SetDestination(_target.position);
            OnVerticalAxis(1.0f);
        }
    }
}
