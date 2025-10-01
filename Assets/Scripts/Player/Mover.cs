using UnityEngine;

public class Mover : MonoBehaviour
{
    private const float SPEED_COEFFICIENT = 50;

    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private int _dashCoefficient;

    private Rigidbody2D _rigB;

    private void Start()
    {
        _rigB = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 Dirrection, bool _isDash)
    {
        _rigB.linearVelocity = Dirrection * _speed * SPEED_COEFFICIENT * Time.fixedDeltaTime;
        float actualSpeed = _rigB.linearVelocity.magnitude;

        if (_isDash == true)
            _rigB.linearVelocity *= _dashCoefficient;
    }

    public void Walk(Transform target) => Move(target, _speed);

    public void Run(Transform target) => Move(target, _runSpeed);

    private void Move(Transform target, float speed)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        _rigB.MovePosition(newPosition);
    }

}