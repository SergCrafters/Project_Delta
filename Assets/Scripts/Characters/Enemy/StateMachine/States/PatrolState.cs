using UnityEngine;

class PatrolState : State, IMoveState
{
    private WayPoint[] _wayPoints;
    private Mover _mover;
    private EnemyVision _vision;
    private BackToPoint _backToPoint;
    private EnemySound _sound;
    private AnimatorController _animatorController;
    private Transform _target;
    private int _wayPointIndex;

    public PatrolState(StateMachine stateMachine, Mover mover, EnemyVision vision, BackToPoint backToPoint ,EnemySound sound, AnimatorController animatorController, LayerMask waypointLayer, WayPoint[] wayPoints,
                            float maxSqrDistance, Transform transform, float sqrAttackDistance) : base(stateMachine)
    {
        _mover = mover;
        _vision = vision;
        _backToPoint = backToPoint;
        _sound = sound;
        _wayPoints = wayPoints;
        _animatorController = animatorController;

        Transitions = new Transition[]
        {
                new SeeTargetTransition(stateMachine, vision, waypointLayer, vision.transform, sqrAttackDistance),
                new WayPointReachedTransition(stateMachine, this, maxSqrDistance, transform)
        };
    }

    public Transform Target => _target;

    public override void Enter(State previousState)
    {
        if (_wayPointIndex == -1)
        {
            _wayPointIndex = _backToPoint.FindIndexClosestWayPoint(_wayPoints, _wayPointIndex, _target);
            _target = _wayPoints[_wayPointIndex].transform;
        }
        else
            _wayPointIndex = _backToPoint.ChangeTarget(_wayPoints, _wayPointIndex, _target);
            _target = _wayPoints[_wayPointIndex].transform;
    }

    public override void Exit(State nextState)
    {
        if (nextState is FollowState)
            _wayPointIndex = -1;
    }

    public override void Update()
    {
        _mover.Walk(_target);
        _vision.LookAtTarget(_target.position);
        _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy, isWalk: true);
        _sound.PlayStepSound();
    }
}

