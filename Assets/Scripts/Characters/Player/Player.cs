using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerAttacker), typeof(Mover))]
[RequireComponent(typeof(AnimatorController), typeof(CollisionHandler))]

public class Player : Character
{
    [SerializeField] AnimationEvent _animationEvent;

    private InputReader _inputReader;
    private PlayerAttacker _attacker;
    private Mover _mover;
    private AnimatorController _AnimatorController;
    private CollisionHandler _collisionHandler;
    public bool _isDash;

    private IInteractable _interactable;

    private bool _isAttack;
    private Vector2 _lastDirection;


    protected override void Awake()
    {
        base.Awake();

        _inputReader = GetComponent<InputReader>();
        _attacker = GetComponent<PlayerAttacker>();
        _mover = GetComponent<Mover>();
        _AnimatorController = GetComponent<AnimatorController>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        _collisionHandler.FinishReached += OnFinishReached;
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackStarted += _attacker.OnCanAttack;
        _animationEvent.AttackEnded += _attacker.OnCanAttack;
        _collisionHandler.FinishReached += OnFinishReached;

    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _collisionHandler.FinishReached -= OnFinishReached;
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackStarted -= _attacker.OnCanAttack;
        _animationEvent.AttackEnded -= _attacker.OnCanAttack;
        _collisionHandler.FinishReached -= OnFinishReached;

    }

    void Update()
    {
        _isDash = _inputReader.GetIsDash();

        if (_inputReader.Dirrection != Vector2.zero)
        {
            _lastDirection = _inputReader.Dirrection.normalized;
        }

        _AnimatorController.UpdateAnimationParameters(_inputReader.Dirrection, _isAttack = false, _isDash);
    }

    private void FixedUpdate()
    {
        if (_attacker.canAction)
        {
            if (_inputReader.Dirrection != null)
            {
                _mover.Move(_inputReader.Dirrection, _inputReader.GetIsDash());
            }

            if (_inputReader.GetIsAttack())
            {
                _attacker.UpdateAttackZone(_lastDirection);
                _AnimatorController.UpdateAnimationParameters(_inputReader.Dirrection, _isAttack = true, _isDash);
            }
        }

        else
            _mover.Move(Vector2.zero, _inputReader.GetIsDash());

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnFinishReached(IInteractable finish) => _interactable = finish;

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

    protected override void OnTakingDamage()
    {
        ///расскомментировать, когда довавится анимация получения урона игрока
        //if (_attacker.canAction == false)
        //    _attacker.OnCanAttack();
    }

}