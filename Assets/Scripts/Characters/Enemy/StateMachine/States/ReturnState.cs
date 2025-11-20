using UnityEngine;

class ReturnState : State
{
    private BackToPoint _backToPoint;
    private Mover _mover;
    private EnemyVision _vision;
    private EnemySound _sound;
    private LayerMask _waypointLayer;
    private WayPoint[] _wayPoints;
    private AnimatorController _animatorController;

    public ReturnState(StateMachine stateMachine, BackToPoint backToPoint, Mover mover, EnemyVision vision, EnemySound sound,
                        AnimatorController animatorController, LayerMask waypointLayer, WayPoint[] wayPoints, float sqrAttackDistance) : base(stateMachine)
    {
        _mover = mover;
        _vision = vision;
        _sound = sound;
        _backToPoint = backToPoint;
        _waypointLayer = waypointLayer;
        _wayPoints = wayPoints;
        _animatorController = animatorController;

        Transitions = new Transition[]
        {
                new SeeTargetTransition(stateMachine, vision, waypointLayer, vision.transform, sqrAttackDistance),
                new ReturnedTransition(stateMachine, backToPoint)

        };
    }

    public override void Enter(State previousState) => _backToPoint.FindPathToRedPoint(_waypointLayer, _wayPoints);

    public override void Update()
    {
        Transform target = _backToPoint.GetNextTarget();

        if (target != null)
        {
            _mover.Walk(target);
            _vision.LookAtTarget(target.position);
            _sound.PlayStepSound();
        }
        _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy, isWalk: true);
    }

    public override void Exit(State nextState) => _backToPoint.ClearPath();
}

