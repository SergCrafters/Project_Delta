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

    private Health _health;
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _health = new Health(_maxHealth);
    }

    private void Start()
    {
        var mover = GetComponent<Mover>();
        var vision = GetComponent<EnemyVision>();
        var animatorController = GetComponent<AnimatorController>();
        var backToPoint = GetComponent<BackToPoint>();
        var attacker = GetComponent<EnemyAttacker>();

        _stateMachine = new EnemyStateMachine(mover, vision, animatorController, backToPoint, attacker, _waypointLayer, _wayPoints, _maxSqrDistance, transform, _waitTime, _sqrAttackDistance);
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
}

