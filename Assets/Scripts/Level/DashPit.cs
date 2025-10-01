using UnityEngine;

public class DashPit : MonoBehaviour
{
    [SerializeField] private Collider2D _pitCollider;
    [SerializeField] private bool _isPlayerInPit = false;
    private Player _player;

    private void Start()
    {
        _pitCollider = GetComponent<Collider2D>();
        _pitCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            _isPlayerInPit = true;

            if (_player != null && !_player._isDash)
                _player.GameOver();
        }

        if (other.CompareTag("Enemy"))
            _pitCollider.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInPit = false;
            _pitCollider.isTrigger = true;
        }
    }

    private void Update()
    {
        if (_isPlayerInPit && _player != null && !_player._isDash)
            _player.GameOver();
       

        if (_player != null && !_player._isDash && _pitCollider.isTrigger)
            _isPlayerInPit = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            _pitCollider.isTrigger = true;
    }
}
