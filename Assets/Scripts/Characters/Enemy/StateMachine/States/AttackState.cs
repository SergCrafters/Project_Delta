using Unity.VisualScripting;
using UnityEngine;

class AttackState : State
{
    private EnemyAttacker _attacker;
    private AnimatorController _animator;
    private EnemyVision _vision;
    private EnemySound _sound;
    private LayerMask _waypointLayer;
    private Transform _target;


    public AttackState(StateMachine stateMachine, EnemyAttacker attacker, AnimatorController animator, EnemyVision vision, EnemySound sound, float tryFindTime, LayerMask waypointLayer, float sqrAttackDistance) : base(stateMachine)
    {
        _attacker = attacker;
        _animator = animator;
        _vision = vision;
        _waypointLayer = waypointLayer;
        _sound = sound;

        Transitions = new Transition[]
         {
            new SeeTargetTransition(stateMachine, vision, waypointLayer, vision.transform, sqrAttackDistance),
            new LostTargetTransition(stateMachine, vision, waypointLayer)
     };
    }

    public override void Enter(State previousState)
    {
        _vision.TrySeeTarget(out _target, _waypointLayer);
    }

    public override void Update()
    {
        _animator.UpdateAnimationParametersEnemy(_vision.GetVisionDirection());

        if (_target != null)
        {
            _vision.LookAtTarget(_target.position);
        }

        if (_attacker.CanAttack)
        {
            _attacker.SetAttackDirection(_vision.GetVisionDirection());
            _attacker.StartAttack();
            _animator.UpdateAnimationParametersEnemy(_vision.GetVisionDirection(), isAttack: true);
            _sound.PlayAttackSound();

        }
    }
}
