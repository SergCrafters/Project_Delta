
using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
    {
        public EnemyStateMachine(Mover mover, EnemyVision vision, AnimatorController animatorController, BackToPoint backToPoint, LayerMask waypointLayer, WayPoint[] wayPoints,
                                float maxSqrDistance, Transform transform, float waitTime)
        {
            States = new Dictionary<Type, State>()
            {
                {typeof(PatrolState), new PatrolState(this, mover, vision, animatorController, waypointLayer, wayPoints, maxSqrDistance, transform) },
                {typeof(IdleState), new IdleState(this, mover, vision, animatorController, waypointLayer, waitTime) },
                {typeof(FollowState), new FollowState(this, vision, animatorController, mover, waypointLayer) },
                {typeof(ReturnState), new ReturnState(this, backToPoint, mover, vision, animatorController, waypointLayer, wayPoints) },

            };

            ChacgeState<PatrolState>();
        }
    }

