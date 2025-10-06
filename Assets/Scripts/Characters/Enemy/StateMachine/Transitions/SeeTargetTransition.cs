using UnityEngine;

class SeeTargetTransition : Transition
    {
        private EnemyVision _vision;
        private LayerMask _waypointLayer;

        public SeeTargetTransition(StateMachine stateMachine, EnemyVision vision, LayerMask waypointLayer) : base(stateMachine)
        {
            _waypointLayer = waypointLayer;
            _vision = vision;
        }

        public override bool IsNeedTransit() => _vision.TrySeeTarget(out Transform _, _waypointLayer);

        public override void Transit() => StateMachine.ChacgeState<FollowState>();
    }

