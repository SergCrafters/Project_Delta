using UnityEngine;

public class Noiser : MonoBehaviour
{
    [SerializeField] private float noiseRadius = 4f;
    [SerializeField] private float noiseDuration = 1f;

    private float _noiseTimer = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, noiseRadius);
    }

    private void Update()
    {
        if (_noiseTimer > 0)
        {
            _noiseTimer -= Time.deltaTime;
            CreateNoise();
        }
    }

    public void CreateNoise()
    {
        Collider2D[] listeners = Physics2D.OverlapCircleAll(transform.position, noiseRadius);

        foreach (Collider2D listener in listeners)
        {
            if (listener.TryGetComponent(out Enemy enemy))
            {
                enemy.Hear(transform.position);
            }
        }
    }
}
