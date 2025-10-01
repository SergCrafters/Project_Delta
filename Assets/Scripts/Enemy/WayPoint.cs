using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public bool isRedPoint = false;
    public WayPoint[] connectedPoints;
    public float searchRadius = 5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = isRedPoint ? Color.red : Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);

        Gizmos.color = Color.white;
        foreach (var point in connectedPoints)
        {
            if (point != null)
                Gizmos.DrawLine(transform.position, point.transform.position);
        }
    }
}
