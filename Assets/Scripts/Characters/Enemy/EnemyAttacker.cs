using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float _delay = 4;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _offsetDistance = 0.5f;
    [SerializeField] private LayerMask _targetLayer;

    private float _endWaitTime;
    private Vector2 _attackDirection = Vector2.right; // направление по умолчанию

    public float Delay => _delay;
    public bool CanAttack => _endWaitTime <= Time.time;
    public bool IsAttack { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackOrigin(), _radius);
    }

    public void Attack()
    {
        print("атака врага");
        Collider2D hit = Physics2D.OverlapCircle(GetAttackOrigin(), _radius, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage);
        }
    }

    // Enemy передает направление взгляда
    public void SetAttackDirection(Vector2 direction)
    {
        _attackDirection = direction.normalized;
    }

    public void StartAttack()
    {
        IsAttack = true;
        _endWaitTime = Time.time + _delay;
    }

    public void OnAttackEnded()
    {
        IsAttack = false;
    }

    private Vector2 GetAttackOrigin()
    {
        return (Vector2)transform.position + _attackDirection * _offsetDistance;
    }
}