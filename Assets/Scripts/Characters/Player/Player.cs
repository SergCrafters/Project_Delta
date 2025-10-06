using UnityEngine;

    [RequireComponent(typeof(InputReader), typeof(PlayerAttack), typeof(Mover))]
    [RequireComponent(typeof(AnimatorController), typeof(CollisionHandler))]

public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerAttack _attack;
    private Mover _mover;
    private AnimatorController _AnimatorController;
    private CollisionHandler _collisionHandler;
    public bool _isDash;

    private IInteractable _interactable;

    private bool _isAttack;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _attack = GetComponent<PlayerAttack>();
        _mover = GetComponent<Mover>();
        _AnimatorController = GetComponent<AnimatorController>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        _isAttack = _inputReader.GetIsAttack();  
        _isDash = _inputReader.GetIsDash();

        _AnimatorController.UpdateAnimationParameters(_inputReader.Dirrection,_isAttack, _isDash);
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
            _mover.Move(_inputReader.Dirrection, _inputReader.GetIsDash());

        else
            _attack.Attack();

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnFinishReached(IInteractable finish)
    {
        _interactable = finish;
    }

    public void Finish()
    {
        Debug.Log("Вы победили");
        gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("Вы проиграли");
        gameObject.SetActive(false);
    }
}