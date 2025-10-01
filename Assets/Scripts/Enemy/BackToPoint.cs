using System.Collections.Generic;
using UnityEngine;

public class BackToPoint : MonoBehaviour
{
    [SerializeField] private float _arrivalThreshold = 0.5f;
    [SerializeField] private float _searchRadius = 10f;

    private List<Transform> _currentPath = new List<Transform>();
    private int _currentPathIndex = 0;

    public void FindPathToRedPoint(LayerMask waypointLayer, WayPoint[] _wayPoints)
    {
        _currentPathIndex = 0;
        _currentPath.Clear();

        Collider2D[] nearbyPoints = Physics2D.OverlapCircleAll(transform.position, _searchRadius, waypointLayer);
        if (nearbyPoints.Length == 0) return;

        WayPoint startPoint = null;
        float minDistance = float.MaxValue;

        foreach (var point in nearbyPoints)
        {
            WayPoint waypoint = point.GetComponent<WayPoint>();
            if (waypoint != null)
            {
                float distance = Vector2.Distance(transform.position, point.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    startPoint = waypoint;
                }
            }
        }

        if (startPoint != null)
        {
            _currentPath = FindPathToRed(startPoint, _wayPoints);
        }
    }

    public Transform GetNextTarget()
    {
        if (_currentPath.Count == 0 || _currentPathIndex >= _currentPath.Count)
            return null;

        Transform currentTarget = _currentPath[_currentPathIndex];

        float distance = Vector2.Distance(transform.position, currentTarget.position);
        if (distance <= _arrivalThreshold)
        {
            _currentPathIndex++;

            if (_currentPathIndex < _currentPath.Count)
            {
                return _currentPath[_currentPathIndex];
            }
            else
            {
                return null;
            }
        }

        return currentTarget;
    }

    private List<Transform> FindPathToRed(WayPoint startPoint, WayPoint[] _wayPoints)
    {
        Queue<WayPoint> queue = new Queue<WayPoint>();
        Dictionary<WayPoint, WayPoint> cameFrom = new Dictionary<WayPoint, WayPoint>();
        HashSet<WayPoint> visited = new HashSet<WayPoint>();

        queue.Enqueue(startPoint);
        visited.Add(startPoint);
        cameFrom[startPoint] = null;

        WayPoint redPoint = null;

        while (queue.Count > 0)
        {
            WayPoint current = queue.Dequeue();

            foreach (WayPoint _wayPoint in _wayPoints)
            {
                if (_wayPoint == current)
                {
                    redPoint = current;
                    break;
                }
            }

            foreach (WayPoint neighbor in current.connectedPoints)
            {
                if (neighbor != null && !visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        List<Transform> path = new List<Transform>();
        if (redPoint != null)
        {
            WayPoint current = redPoint;
            while (current != null)
            {
                path.Insert(0, current.transform);
                current = cameFrom.ContainsKey(current) ? cameFrom[current] : null;
            }
        }

        return path;
    }

    public bool IsReturned()
    {
        return _currentPathIndex >= _currentPath.Count;
    }

    public void ClearPath()
    {
        _currentPath.Clear();
        _currentPathIndex = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);

        if (_currentPath != null && _currentPath.Count > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < _currentPath.Count - 1; i++)
            {
                if (_currentPath[i] != null && _currentPath[i + 1] != null)
                {
                    Gizmos.DrawLine(_currentPath[i].position, _currentPath[i + 1].position);
                }
            }
        }
    }
}
