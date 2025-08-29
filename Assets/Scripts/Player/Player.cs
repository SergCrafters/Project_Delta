using UnityEngine;

    [RequireComponent(typeof(InputReader), typeof(PlayerAttack), typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerAnimatorController))]

public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerAttack _attack;
    private PlayerMover _mover;
    private PlayerAnimatorController _AnimatorController;

    private bool _isAttack;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _attack = GetComponent<PlayerAttack>();
        _mover = GetComponent<PlayerMover>();
        _AnimatorController = GetComponent<PlayerAnimatorController>();
    }

    void Update()
    {
        _isAttack = _inputReader.GetIsAttack();  
    }

    private void FixedUpdate()
    {
        if (!_isAttack)
            _mover.Movement(_inputReader.Dirrection, _inputReader.GetIsDash());

        else
            _attack.Attack();
    }
}
