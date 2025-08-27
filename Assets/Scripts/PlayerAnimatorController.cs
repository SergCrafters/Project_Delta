using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    [Header("Animator References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Player_Mover _playerMover;

    [Header("Animation Settings")]
    [SerializeField] private float movementThreshold = 0.1f;

    private const int DIRECTION_LEFT = 0;
    private const int DIRECTION_UP = 1;
    private const int DIRECTION_RIGHT = 2;
    private const int DIRECTION_DOWN = 3;

    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private int currentDirection = DIRECTION_RIGHT;
    private bool isMoving;

    private void Start()
    {
        animator.SetBool("IsMove", false);
        animator.SetInteger("LastDirection", DIRECTION_RIGHT);
    }
    private void FixedUpdate()
    {
        UpdateAnimationParameters();
    }

    private void UpdateAnimationParameters()
    {
        Vector2 movementInput = new Vector2(
            Input.GetAxisRaw(HORIZONTAL_AXIS),
            Input.GetAxisRaw(VERTICAL_AXIS)
        );


        isMoving = movementInput.magnitude > movementThreshold;
        bool _isAttack = _playerMover.GetIsAttack();
        animator.SetInteger("Action", _isAttack ? 2 : isMoving ? 1 : 0);

        if (isMoving)
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
