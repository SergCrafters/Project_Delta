using UnityEngine;

class IdleState : State
{
    private float _endWaitTime;
    private float _waitTime;
    private AnimatorController _animatorController;
    private EnemyVision _vision;


    public IdleState(StateMachine stateMachine, Mover mover, EnemyVision vision, AnimatorController animatorController, LayerMask waypointLayer, float waitTime, float sqrAttackDistance) : base(stateMachine)
    {
        _waitTime = waitTime;
        _animatorController = animatorController;
        _vision = vision;



        Transitions = new Transition[]
        {
                new SeeTargetTransition(stateMachine, vision, waypointLayer, vision.transform, sqrAttackDistance),
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
        _animatorController.UpdateAnimationParametersEnemy(_vision.GetVisionDirection());
    }
}

