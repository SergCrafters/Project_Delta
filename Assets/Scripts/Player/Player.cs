using UnityEngine;

    [RequireComponent(typeof(InputReader), typeof(PlayerAttack), typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerAnimatorController), typeof(CollisionHandler))]

public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerAttack _attack;
    private PlayerMover _mover;
    private PlayerAnimatorController _AnimatorController;
    private CollisionHandler _collisionHandler;

    private IInteractable _interactable;

    private bool _isAttack;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _attack = GetComponent<PlayerAttack>();
        _mover = GetComponent<PlayerMover>();
        _AnimatorController = GetComponent<PlayerAnimatorController>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        _isAttack = _inputReader.GetIsAttack();  
    }

    private void OnEnable()
    {
        _collisionHandler.FinishReached += OnFinishReached;
    }

    private void OnDisable()
    {
        _collisionHandler.FinishReached -= OnFinishReached;
    }

    private void FixedUpdate()
    {
        if (!_isAttack)
            _mover.Movement(_inputReader.Dirrection, _inputReader.GetIsDash());

        else
            _attack.Attack();

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }


    private void OnFinishReached(IInteractable finish)
    {
        _interactable = finish;
    }
}
