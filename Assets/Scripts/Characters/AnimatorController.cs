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
        animator.SetInteger(ConstantData.AnimatorParameters.LastDirection, currentDirection);
    }

    public void UpdateAnimationParameters(Vector2 movementInput, bool isDash, bool isAttack = false, bool isHit = false)
    {
        animator.SetInteger(ConstantData.AnimatorParameters.Action, isDash ? ConstantData.ActionParameters.DASH :
                            _isMoving ? ConstantData.ActionParameters.WALK : ConstantData.ActionParameters.IDLE);

        if (isAttack)
            animator.SetTrigger(ConstantData.AnimatorParameters.IsAttack);

        if (isHit)
            animator.SetTrigger(ConstantData.AnimatorParameters.IsHit);

        UpdateDirection(movementInput);
    }

    public void UpdateAnimationParametersEnemy(Vector2 movementInput, bool isRun = false,
                bool isWalk = false, bool isAttack = false, bool isHit = false, bool isDeath = false)
    {
        animator.SetInteger(ConstantData.AnimatorParameters.Action, isRun ? ConstantData.ActionParameters.RUN :
                            isWalk ? ConstantData.ActionParameters.WALK : ConstantData.ActionParameters.IDLE);

        if (isAttack)
            animator.SetTrigger(ConstantData.AnimatorParameters.IsAttack);

        if (isHit)
            animator.SetTrigger(ConstantData.AnimatorParameters.IsHit);

        if (isDeath)
            animator.SetTrigger(ConstantData.AnimatorParameters.IsDeath);

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
                animator.SetInteger(ConstantData.AnimatorParameters.LastDirection, currentDirection);
            }
        }
    }

    private int CalculateDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return input.x > 0 ? ConstantData.DirectionData.DIRECTION_RIGHT : ConstantData.DirectionData.DIRECTION_LEFT;

        else if (Mathf.Abs(input.y) > 0)
            return input.y > 0 ? ConstantData.DirectionData.DIRECTION_UP : ConstantData.DirectionData.DIRECTION_DOWN;

        return currentDirection;
    }
}
