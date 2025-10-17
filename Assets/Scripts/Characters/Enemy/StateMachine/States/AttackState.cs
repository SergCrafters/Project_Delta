using UnityEngine;

class AttackState : State
{
    private EnemyAttacker _attacker;
    private AnimatorController _animator;
    private EnemyVision _vision;
    private float _sqrAttackDistance;
    private LayerMask _waypointLayer;
    private LostTargetTransition _lostTargetTransition;
    private Transform _target;

    public AttackState(StateMachine stateMachine, EnemyAttacker attacker, AnimatorController animator, EnemyVision vision, float tryFindTime, LayerMask waypointLayer, float sqrAttackDistance) : base(stateMachine)
    {
        _attacker = attacker;
        _animator = animator;
        _vision = vision;
        _sqrAttackDistance = sqrAttackDistance;
        _waypointLayer = waypointLayer;

       //_lostTargetTransition = new LostTargetTransition(stateMachine, vision, tryFindTime);

       Transitions = new Transition[]
        {
            //new SeeTargetTransition(stateMachine, vision, vision.transform, attacker.AttackDistance),
            new SeeTargetTransition(stateMachine, vision, waypointLayer, vision.transform, sqrAttackDistance),
            new LostTargetTransition(stateMachine, vision, waypointLayer)
    };
    }

    public override void Enter(State previousState)
    {
        _vision.TrySeeTarget(out _target, _waypointLayer);
        //_vision.LookAtTarget(_target.position);
        //_lostTargetTransition.IsNeedTransit();
    }

    public override void Update()
    {
        if (_target != null)
        {
            _vision.LookAtTarget(_target.position);
        }
        //Debug.Log("Акатую");
        //if (_attacker.IsAttack == false)
        //    _fliper.LookAtTarget(_target.position);

        //if (_attacker.CanAttack)
        //{
        //    _attacker.StartAttack();
        //    _animator.SetTrigger(ConstantData.AnimatorParameters.Attack);
        //}
    }

    //public override void TryTransit()
    //{
    //    if (_attacker.IsAttack == false)
    //    {
    //        base.TryTransit();
    //    }

    //}
}
