using UnityEngine;

class PatrolState : State, IMoveState
{
    private WayPoint[] _wayPoints;
    private Mover _mover;
    private EnemyVision _vision;
    private EnemySound _sound;
    private AnimatorController _animatorController;
    private Transform _target;
    private int _wayPointIndex;

    public PatrolState(StateMachine stateMachine, Mover mover, EnemyVision vision, EnemySound sound, AnimatorController animatorController, LayerMask waypointLayer, WayPoint[] wayPoints,
                            float maxSqrDistance, Transform transform, float sqrAttackDistance) : base(stateMachine)
    {
        _mover = mover;
        _vision = vision;
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
            FindClosestWayPoint();

        else
            ChangeTarget();
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

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FindClosestWayPoint()
    {

        float minDistance = float.MaxValue;
        int closestIndex = 0;

        for (int i = 0; i < _wayPoints.Length; i++)
        {

            float distance = Vector3.Distance(_mover.transform.position, _wayPoints[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        _wayPointIndex = ++closestIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;
    }
}

