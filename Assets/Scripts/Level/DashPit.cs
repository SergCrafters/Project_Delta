using UnityEngine;

public class DashPit : MonoBehaviour
{
    [SerializeField] private Collider2D _pitCollider;
    [SerializeField] private bool _isPlayerInPit = false;
    [SerializeField] private int _fallDamage = 10;

    private Player _player;

    private void Start()
    {
        _pitCollider = GetComponent<Collider2D>();
        _pitCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && player.GetIsDash())
        {
            _isPlayerInPit = true;
            _player = player;
        }
        else
        {
            _pitCollider.isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _pitCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isPlayerInPit = false;
            _pitCollider.isTrigger = true;
        }
    }

    private void Update()
    {
        if (_isPlayerInPit && _player != null && _player.GetIsDash() == null)
        {
            _player.Fall(_fallDamage);
            _isPlayerInPit = false;
        }
    }
}
