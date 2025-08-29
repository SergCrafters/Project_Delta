using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private const float SPEED_COEFFICIENT = 50;

    [SerializeField] private float _speed;
    [SerializeField] private int _dashCoefficient;

    private Rigidbody2D _rigB;

    private void Start()
    {
        _rigB = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 Dirrection, bool _isDash)
    {
        _rigB.linearVelocity = Dirrection * _speed * SPEED_COEFFICIENT * Time.fixedDeltaTime;

        if (_isDash == true)
            _rigB.linearVelocity *= _dashCoefficient;
    }
}