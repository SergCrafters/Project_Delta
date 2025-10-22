using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyVision), typeof(BackToPoint))]
[RequireComponent(typeof(AnimatorController), typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private LayerMask _waypointLayer;
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _sqrAttackDistance = 1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] AnimationEvent _animationEvent;

    private Health _health;
    private EnemyVision _vision;
    private EnemyAttacker _attacker;
    private AnimatorController _animatorController;
    private EnemyStateMachine _stateMachine;
    private Mover _mover;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _vision = GetComponent<EnemyVision>();
        _attacker = GetComponent<EnemyAttacker>();
        _animatorController = GetComponent<AnimatorController>();
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;
        _health.TakingDamage += OnTakingDamage;
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        var backToPoint = GetComponent<BackToPoint>();

        _stateMachine = new EnemyStateMachine(_mover, _vision, _animatorController, backToPoint, _attacker, _waypointLayer, _wayPoints, _maxSqrDistance, transform, _waitTime, _sqrAttackDistance);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
        print(_health.Value);

        if (_health.Value == 0)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackEnded -= _attacker.OnAttackEnded;
        _health.TakingDamage -= OnTakingDamage;
    }

    private void OnTakingDamage()
    {
        _animatorController.UpdateAnimationParametersEnemy(_mover.DirrectionEnemy, isHit: true);

        //if (_vision.TrySeeTarget(out Transform _target, _waypointLayer) == false)
        //    _vision.LookAtTarget(_target.position);

    }
}

