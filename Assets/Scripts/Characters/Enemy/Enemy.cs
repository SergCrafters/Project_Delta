using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyVision), typeof(BackToPoint))]
[RequireComponent(typeof(AnimatorController), typeof(EnemyAttacker), typeof(EnemySound))]
public class Enemy : Character
{
    [SerializeField] private LayerMask _waypointLayer;
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _sqrAttackDistance = 1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private AnimationEvent _animationEvent;

    private EnemyVision _vision;
    private EnemyAttacker _attacker;
    private EnemySound _sound;
    private AnimatorController _animatorController;
    private EnemyStateMachine _stateMachine;
    private Mover _mover;

    protected override void Awake()
    {
        base.Awake();

        _vision = GetComponent<EnemyVision>();
        _attacker = GetComponent<EnemyAttacker>();
        _sound = GetComponent<EnemySound>();
        _animatorController = GetComponent<AnimatorController>();
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;
        _animationEvent.Death += Destroy;
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        var backToPoint = GetComponent<BackToPoint>();

        _stateMachine = new EnemyStateMachine(_mover, _vision, _animatorController, backToPoint, _attacker, _sound, _waypointLayer, _wayPoints, _maxSqrDistance, transform, _waitTime, _sqrAttackDistance);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    private void OnDestroy()
    {
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackEnded -= _attacker.OnAttackEnded;
    }

    protected override void OnTakingDamage()
    {
        _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy, isHit: true);
        _sound.PlayHitSound();
    }

    public void Hear(Vector3 target)
    {
        if (_vision.TrySeeTarget(out Transform _target, _waypointLayer) == false)
            _vision.LookAtTarget(target);
    }

    protected override void OnDied()
    {
        _sound.PlayDeathSound();
        _mover.isDontMoving = true;
        _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy, isDeath: true);

    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}

