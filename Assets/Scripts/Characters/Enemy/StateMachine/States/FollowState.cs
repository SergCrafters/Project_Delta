using UnityEngine;

class FollowState : State, IMoveState
{
    private EnemyVision _vision;
    private LayerMask _waypointLayer;
    private Mover _mover;
    private Transform _target;
    private AnimatorController _animatorController;

    public FollowState(StateMachine stateMachine, EnemyVision vision, AnimatorController animatorController, Mover mover, LayerMask waypointLayer, float sqrAttackDistance) : base(stateMachine)
    {
        _mover = mover;
        _vision = vision;
        _waypointLayer = waypointLayer;
        _animatorController = animatorController;

        Transitions = new Transition[]
        {
                new LostTargetTransition(stateMachine, vision, _waypointLayer),
                new TargetReachedTransition(stateMachine, this, sqrAttackDistance, mover.transform)

        };
    }

    public Transform Target => _target;

    public override void Enter(State previousState)
    {
        _vision.TrySeeTarget(out _target, _waypointLayer);
        //

    }

    public override void Update()
    {
        if (_target != null)
        {
            _mover.Run(_target);
            _vision.LookAtTarget(_target.position);
        }
        //
        _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy, isRun: true);
    }
}

