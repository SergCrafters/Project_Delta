using UnityEngine;

class SeeTargetTransition : Transition
{
    private EnemyVision _vision;
    private Transform _transform;
    private float _sqrAttackDistance;
    private LayerMask _waypointLayer;


    public SeeTargetTransition(StateMachine stateMachine, EnemyVision vision, LayerMask waypointLayer, Transform transform, float sqrAttackDistance) : base(stateMachine)
    {
        _waypointLayer = waypointLayer;
        _vision = vision;
        _transform = transform;
        _sqrAttackDistance = sqrAttackDistance;
    }

    public override bool IsNeedTransit() => 
        _vision.TrySeeTarget(out Transform target, _waypointLayer) && (_transform.position - target.position).sqrMagnitude > _sqrAttackDistance;

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChacgeState<FollowState>();
    }
}

