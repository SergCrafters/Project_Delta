using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector2 Dirrection { get; private set; }

    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashTime;

    private float _attackTimer;
    private float _dashTimer;
    private bool _isAttack;
    private bool _isDash;

    private void Start()
    {
        _dashTimer = Time.time;
        _attackTimer = Time.time;
    }

    private void Update()
    {
        Dirrection = new Vector2(Input.GetAxis(ConstantData.InpudData.HORIZONTAL_AXIS), Input.GetAxis(ConstantData.InpudData.VERTICAL_AXIS));

        if ((Time.time - _dashTimer > _dashCooldown) && Input.GetKeyDown(KeyCode.Space))
        {
            _isDash = true;
            _dashTimer = Time.time;
        }

        if (Time.time - _dashTimer > _dashTime)
        {
            _isDash = false;
        }

        if ((Time.time - _attackTimer > _attackCooldown) && Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isAttack = true;
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
