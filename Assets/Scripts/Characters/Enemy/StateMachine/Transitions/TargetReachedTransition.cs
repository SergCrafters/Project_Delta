using UnityEngine;

class ReachedTransition : Transition
    {
        private IMoveState _moveState;
        private Transform _transform;
        private float _maxSqrDistance;

        public ReachedTransition(StateMachine stateMachine, IMoveState moveState, float maxSqrDistance, Transform transform) : base(stateMachine)
        {
            _transform = transform;
            _moveState = moveState;
            _maxSqrDistance = maxSqrDistance;
        }

        public override bool IsNeedTransit()
        {
            float sqeDistance = (_transform.position - _moveState.Target.position).sqrMagnitude;

            return sqeDistance < _maxSqrDistance;
        }
}

class TargetReachedTransition : ReachedTransition
{
    public TargetReachedTransition(StateMachine stateMachine, IMoveState moveState, float attackDistance, Transform transform) :
                base(stateMachine, moveState, attackDistance, transform)

    {
    }

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChacgeState<AttackState>();
    }
}

class WayPointReachedTransition : ReachedTransition
{
    public WayPointReachedTransition(StateMachine stateMachine, IMoveState moveState, float maxSqrDistance, Transform transform) :
                base(stateMachine, moveState, maxSqrDistance, transform)
    {

    }

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChacgeState<IdleState>();
    }
}

