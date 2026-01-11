
using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
    {
        public EnemyStateMachine(Mover mover, EnemyVision vision, AnimatorController animatorController, BackToPoint backToPoint, EnemyAttacker attacker, EnemySound sound, LayerMask waypointLayer, WayPoint[] wayPoints,
                                float maxSqrDistance, Transform transform, float waitTime, float sqrAttackDistance)
        {
            States = new Dictionary<Type, State>()
            {
                {typeof(PatrolState), new PatrolState(this, mover, vision, backToPoint, sound, animatorController, waypointLayer, wayPoints, maxSqrDistance, transform, sqrAttackDistance) },
                {typeof(IdleState), new IdleState(this, mover, vision, animatorController, waypointLayer, waitTime, sqrAttackDistance) },
                {typeof(FollowState), new FollowState(this, vision, sound,  animatorController, mover, waypointLayer, sqrAttackDistance) },
                {typeof(ReturnState), new ReturnState(this, backToPoint, mover, vision, sound,  animatorController, waypointLayer, wayPoints, sqrAttackDistance) },
                {typeof(AttackState), new AttackState(this, attacker, animatorController, vision, sound,  2, waypointLayer, sqrAttackDistance) },

            };

            ChacgeState<PatrolState>();
        }
    }

