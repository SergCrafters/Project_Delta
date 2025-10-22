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

