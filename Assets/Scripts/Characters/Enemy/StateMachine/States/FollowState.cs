using UnityEngine;

class FollowState : State
    {
        private EnemyVision _vision;
        private LayerMask _waypointLayer;
        private Mover _mover;
        private Transform _target;
        private AnimatorController _animatorController;

    public FollowState(StateMachine stateMachine, EnemyVision vision, AnimatorController animatorController, Mover mover, LayerMask waypointLayer) : base(stateMachine)
        {
            _mover = mover;
            _vision = vision;
            _waypointLayer = waypointLayer;
            _animatorController = animatorController;

            Transitions = new Transition[]
            {
                new LostTargetTransition(stateMachine, vision, _waypointLayer),
            };
        }

        public override void Enter(State previousState)
        {
            _vision.TrySeeTarget(out _target, _waypointLayer);
        }

        public override void Update()
        {
            if (_target != null)
            {
                _mover.Run(_target);
                _vision.LookAtTarget(_target.position);
            }
            _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy, isRun: true);
        }
    }

