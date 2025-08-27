using UnityEngine;

public class Player_Mover : MonoBehaviour
{
    private const float SPEED_COEFFICIENT = 50;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private const int DASH_TIME = 10;
    private const int DASH_COOLDOWN = 100;

    [SerializeField] private float _speed = 1;
    [SerializeField] private int _dashCoefficient = 5;

    private Rigidbody2D _rigB;
    private int _dashTime;
    private int _dashCooldown;
    private int _attackTimer;
    [SerializeField] private float _attackCooldown;
    private float _timerAttack;

    private bool _isAttack;

    private void Start()
    {
        _rigB = GetComponent<Rigidbody2D>();
        _timerAttack = Time.time;

    }


    private void Update()
    {
        if (_dashCooldown == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _dashTime = DASH_TIME;
            _dashCooldown = DASH_COOLDOWN;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isAttack = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_isAttack)
        {
            Movement();
        }
        else
        {
            Attack();
        }
    }

    private void Movement()
    {
        _rigB.linearVelocity = new Vector2(Input.GetAxis(HORIZONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS)) * _speed * SPEED_COEFFICIENT * Time.fixedDeltaTime;
        if (_dashTime > 0)
        {
            _rigB.linearVelocity *= _dashCoefficient;
            _dashTime--;
        }
        else if (_dashCooldown > 0)
        {
            _dashCooldown--;
        }
    }

    private void Attack()
    {
        if (Time.time - _timerAttack > _attackCooldown)
        {
            _rigB.linearVelocity = new Vector2(0, 0);
            _timerAttack = Time.time;
        }
        else
        {
            _isAttack = false;
        }
    }

    public bool GetIsAttack()
    {
        return _isAttack;
    }
}