using UnityEngine;

public class Speedometr : MonoBehaviour
{
    public string playerTag = "Player";
    [SerializeField] private float _needSpeed;
    [SerializeField] private Collider2D _blockingCollider;

    private void Start()
    {
        if (_blockingCollider != null)
        {
            _blockingCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                float actualSpeed = playerRb.linearVelocity.magnitude;
                Debug.Log(actualSpeed);
                if (actualSpeed < _needSpeed)
                {
                    if (_blockingCollider != null)
                    {
                        _blockingCollider.enabled = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            if (_blockingCollider != null)
            {
                _blockingCollider.enabled = false;
            }
        }
    }
}
