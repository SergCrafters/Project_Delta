using UnityEngine;

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

