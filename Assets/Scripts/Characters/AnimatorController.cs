using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementThreshold = 0.1f;
    [SerializeField] private int currentDirection = 2;

    private bool _isMoving;

    private void Start()
    {
        animator.SetInteger("LastDirection", currentDirection);
    }

    public void UpdateAnimationParameters(Vector2 movementInput, bool isAttack, bool isDash)
    {
        animator.SetInteger("Action", isDash ? 3 : isAttack ? 2 : _isMoving ? 1 : 0);

        UpdateDirection(movementInput);
    }

    public void UpdateAnimationParametersEnemy(Vector2 movementInput, bool isRun = false, bool isWalk = false)
    {
        animator.SetInteger("Action", isRun ? 2 : isWalk ? 1 : 0);

        UpdateDirection(movementInput);
    }

    private void UpdateDirection(Vector2 movementInput)
    {
        _isMoving = movementInput.magnitude > movementThreshold;

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
            return input.x > 0 ? ConstantData.DirectionData.DIRECTION_RIGHT : ConstantData.DirectionData.DIRECTION_LEFT;
        }
        else if (Mathf.Abs(input.y) > 0)
        {
            return input.y > 0 ? ConstantData.DirectionData.DIRECTION_UP : ConstantData.DirectionData.DIRECTION_DOWN;
        }

        return currentDirection;
    }
}
