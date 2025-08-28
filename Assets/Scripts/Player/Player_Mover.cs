using UnityEngine;

public class Player_Mover : MonoBehaviour
{
    private const float SPEED_COEFFICIENT = 50;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private int _dashCoefficient;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _attackCooldown;

    private Rigidbody2D _rigB;
    private float _dashTimer;
    private float _attackTimer;
    private bool _isDash;
    private bool _isAttack;



    private void Start()
    {
        _rigB = GetComponent<Rigidbody2D>();
        _attackTimer = Time.time;
        _dashTimer = Time.time;
    }


    private void Update()
    {
        if ((Time.time - _dashTimer > _dashCooldown) && Input.GetKeyDown(KeyCode.Space))
        {
            _isDash = true;
            _dashTimer = Time.time;
        }

        if (Time.time - _dashTimer > _dashTime)
        {
            _isDash = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isAttack = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_isAttack)
            Movement();

        else
            Attack();
    }

    private void Movement()
    {
        _rigB.linearVelocity = new Vector2(Input.GetAxis(HORIZONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS)) * _speed * SPEED_COEFFICIENT * Time.fixedDeltaTime;

        if (_isDash == true)
            _rigB.linearVelocity *= _dashCoefficient;
    }

    private void Attack()
    {
        if (Time.time - _attackTimer > _attackCooldown)
        {
            _rigB.linearVelocity = new Vector2(0, 0);
            _attackTimer = Time.time;
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

    public bool GetIsDash()
    {
        return _isDash;
    }
}