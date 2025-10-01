using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

    [RequireComponent(typeof(InputReader), typeof(PlayerAttack), typeof(Mover))]
    [RequireComponent(typeof(PlayerAnimatorController), typeof(CollisionHandler))]

public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerAttack _attack;
    private Mover _mover;
    private PlayerAnimatorController _AnimatorController;
    private CollisionHandler _collisionHandler;
    private Finish _finish;
    public bool _isDash;

    private IInteractable _interactable;

    private bool _isAttack;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _attack = GetComponent<PlayerAttack>();
        _mover = GetComponent<Mover>();
        _AnimatorController = GetComponent<PlayerAnimatorController>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _finish = Object.FindAnyObjectByType<Finish>();
    }

    void Update()
    {
        _isAttack = _inputReader.GetIsAttack();  
        _isDash = _inputReader.GetIsDash();
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

        //if(_finish._isFinish)
        //    gameObject.SetActive(false);
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