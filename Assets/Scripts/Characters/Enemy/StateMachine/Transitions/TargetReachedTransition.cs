using UnityEngine;

class TargetReachedTransition : Transition
    {
        private PatrolState _patrolState;
        private Transform _transform;
        private float _maxSqrDistance;

        public TargetReachedTransition(StateMachine stateMachine, PatrolState patrolState, float maxSqrDistance, Transform transform) : base(stateMachine)
        {
            _transform = transform;
            _patrolState = patrolState;
            _maxSqrDistance = maxSqrDistance;
        }

        public override bool IsNeedTransit()
        {
            float sqeDistance = (_transform.position - _patrolState.Target.position).sqrMagnitude;

            return sqeDistance < _maxSqrDistance;
        }

        public override void Transit() => StateMachine.ChacgeState<IdleState>();
    }

