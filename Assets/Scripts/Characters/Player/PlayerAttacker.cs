using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public bool canAttack = true;

    public float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private float _offsetDistance;
    [SerializeField] private LayerMask _targetLayer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(_lastAttackDirection), _radius);
    }

    public Vector2 _lastAttackDirection = Vector2.down;

    public void Attack(Vector2 attackDirection)
    {
        _lastAttackDirection = attackDirection;

        print("атака игрока");
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetAttackOrigin(_lastAttackDirection), _radius, _targetLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit != null && hit.TryGetComponent(out Enemy enemy))
            {
                print("попадание");
                //enemy.ApplyDamage(_damage);
            }
        }
    }

    public Vector2 GetAttackOrigin(Vector2 direction)
    {
        return (Vector2)transform.position + direction * _offsetDistance;
    }

    public void OnCanAttack()
    {
        canAttack = !canAttack;
    }
}
