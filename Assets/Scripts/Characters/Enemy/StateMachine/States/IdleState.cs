using UnityEngine;

class IdleState : State
    {
        private float _endWaitTime;
        private float _waitTime;
        private AnimatorController _animatorController;
        private Mover _mover;

    public IdleState(StateMachine stateMachine, Mover mover, EnemyVision vision, AnimatorController animatorController, LayerMask waypointLayer, float waitTime) : base(stateMachine)
        {
            _waitTime = waitTime;
            _mover = mover;
            _animatorController = animatorController;

            Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine, vision, waypointLayer),
                new EndIdleTransition(stateMachine, this)
            };
        }

        public bool IsEndWait => _endWaitTime <= Time.time;

        public override void Enter(State previousState)
        {
            _endWaitTime = Time.time + _waitTime;
        }

    public override void Update()
    {
        _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy);
    }
    }

