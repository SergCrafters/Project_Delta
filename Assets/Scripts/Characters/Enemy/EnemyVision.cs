using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] private float _visionAngle = 90f;
    [SerializeField] private float _visionDistance = 5f;

    [Header("References")]
    [SerializeField] private Transform _visionPivot;


    private float _currentVisionAngle = 0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _visionDistance);

        Gizmos.color = Color.yellow;
        if (Application.isPlaying)
        {
            Vector2 visionDir = GetVisionDirection();
            Vector2 leftBound = Quaternion.Euler(0, 0, -_visionAngle / 2) * visionDir * _visionDistance;
            Vector2 rightBound = Quaternion.Euler(0, 0, _visionAngle / 2) * visionDir * _visionDistance;

            Gizmos.DrawLine(transform.position, (Vector2)transform.position + visionDir * _visionDistance);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + leftBound);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + rightBound);
        }
    }

    public bool TrySeeTarget(out Transform target, LayerMask waypointLayer)
    {
        target = null;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _visionDistance, _targetLayer);

        if (hit != null)
        {
            Vector2 directionToTarget = (hit.transform.position - transform.position).normalized;
            float angleToTarget = Vector2.Angle(GetVisionDirection(), directionToTarget);

            if (angleToTarget < _visionAngle / 2f)
            {
                LayerMask raycastMask = ~((1 << gameObject.layer) | waypointLayer);
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, directionToTarget, _visionDistance, raycastMask);

                if (hit2D.collider != null && hit2D.collider == hit)
                {
                    target = hit2D.transform;
                    return true;
                }
            }
        }
        return false;
    }

    public void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _currentVisionAngle = Mathf.LerpAngle(_currentVisionAngle, targetAngle, 10f * Time.fixedDeltaTime);
        UpdateVisionRotation();
    }

    public void UpdateVisionRotation()
    {
        if (_visionPivot != null)
        {
            _visionPivot.rotation = Quaternion.Euler(0f, 0f, _currentVisionAngle);
        }
    }

    private Vector2 GetVisionDirection()
    {
        float angleRad = _currentVisionAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
