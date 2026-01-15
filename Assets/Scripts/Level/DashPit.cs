using UnityEngine;

public class DashPit : MonoBehaviour
{
    [SerializeField] private Collider2D _pitCollider;
    [SerializeField] private bool _isPlayerInPit = false;
    [SerializeField] private int _fallDamage = 10;

    private Dash _dash;


    private void Start()
    {
        _pitCollider = GetComponent<Collider2D>();
        _pitCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Dash dash) && dash.GetIsDash())
        {
            _isPlayerInPit = true;
            _dash = dash;
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
        if (collision.TryGetComponent(out Dash dash))
        {
            _isPlayerInPit = false;
            _pitCollider.isTrigger = true;
        }
    }

    private void Update()
    {
        print(_isPlayerInPit);

        if (_isPlayerInPit && _dash != null && !_dash.GetIsDash())
        {
            _dash.ReturnToSafeZone(_fallDamage);
            _isPlayerInPit = false;
        }
    }

}
