using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    public const int DIRECTION_LEFT = 0;
    public const int DIRECTION_UP = 1;
    public const int DIRECTION_RIGHT = 2;
    public const int DIRECTION_DOWN = 3;

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private Mover _playerMover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float movementThreshold = 0.1f;

    private int currentDirection = DIRECTION_RIGHT;
    private bool _isMoving;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        animator.SetInteger("LastDirection", DIRECTION_RIGHT);
    }
    private void Update()
    {
        UpdateAnimationParameters();
    }

    private void UpdateAnimationParameters()
    {
        Vector2 movementInput = _inputReader.Dirrection;

        _isMoving = movementInput.magnitude > movementThreshold;
        bool _isAttack = _inputReader.GetIsAttack();
        bool _isDash = _inputReader.GetIsDash();

        animator.SetInteger("Action", _isDash ? 3 : _isAttack ? 2 : _isMoving ? 1 : 0);

        if (_isMoving)
        {
            int newDirection = CalculateDirection(movementInput);

            if (newDirection != currentDirection)
            {
                currentDirection = newDirection;
                animator.SetInteger("LastDirection", currentDirection);
            }
        }
    }

    private int CalculateDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            return input.x > 0 ? DIRECTION_RIGHT : DIRECTION_LEFT;
        }
        else if (Mathf.Abs(input.y) > 0)
        {
            return input.y > 0 ? DIRECTION_UP : DIRECTION_DOWN;
        }

        return currentDirection;
    }
}
