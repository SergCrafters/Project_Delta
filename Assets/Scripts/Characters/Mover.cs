using UnityEngine;

public class Mover : MonoBehaviour
{
    private const float SPEED_COEFFICIENT = 50;

    public Vector2 DirrectionEnemy { get; private set; }

    private bool _isMoving = true;

    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private int _dashCoefficient;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 Dirrection, bool isDash)
    {
        _rigidbody.linearVelocity = Dirrection * _speed * SPEED_COEFFICIENT * Time.fixedDeltaTime;
        float actualSpeed = _rigidbody.linearVelocity.magnitude;

        if (isDash == true)
            _rigidbody.linearVelocity *= _dashCoefficient;
    }

    public void Walk(Transform target) => 
        Move(target, _speed);

    public void Run(Transform target) => 
        Move(target, _runSpeed);

    public void ÑhangeIsMoving(bool isMoving) => 
        _isMoving = isMoving;

    private void Move(Transform target, float speed)
    {
        if (_isMoving)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            DirrectionEnemy = (newPosition - (Vector2)transform.position).normalized;
            _rigidbody.MovePosition(newPosition);
        }
    }
}