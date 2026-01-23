using System;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerAttacker), typeof(Mover))]
[RequireComponent(typeof(AnimatorController), typeof(CollisionHandler), typeof(PlayerSound))]
[RequireComponent(typeof(Dash), typeof(Noiser))]

public class Player : Character
{
    [SerializeField] private AnimationEvent _animationEvent;
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;

    private InputReader _inputReader;
    private PlayerAttacker _attacker;
    private PlayerSound _sound;
    private Mover _mover;
    private Dash _dash;
    private Noiser _noiser;
    private AnimatorController _AnimatorController;
    private CollisionHandler _collisionHandler;
    public bool _isDash;

    private Inventory _inventory;

    private IInteractable _interactable;

    private bool _isAttack;
    private Vector2 _lastDirection;


    protected override void Awake()
    {
        base.Awake();

        _inputReader = GetComponent<InputReader>();
        _attacker = GetComponent<PlayerAttacker>();
        _mover = GetComponent<Mover>();
        _dash = GetComponent<Dash>();
        _noiser = GetComponent<Noiser>();
        _AnimatorController = GetComponent<AnimatorController>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _sound = GetComponent<PlayerSound>();

        _inventory = new Inventory();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        _collisionHandler.InteractableFounded += OnInteractableFounded;
        _collisionHandler.MedKitFounded += OnMedKitFounded;
        _collisionHandler.KeyFounded += OnKeyFounded;

        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackStarted += _attacker.OnCanAttack;
        _animationEvent.AttackEnded += _attacker.OnCanAttack;
        _animationEvent.DashStart += _sound.PlayDashSound;


        _inventory.itemAdded += AddItemToInventory;
        _inventory.itemRemoved += _inventoryView.Remove;

        _dash.Return += ApplyDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _collisionHandler.InteractableFounded -= OnInteractableFounded;
        _collisionHandler.MedKitFounded -= OnMedKitFounded;
        _collisionHandler.KeyFounded -= OnKeyFounded;

        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackStarted -= _attacker.OnCanAttack;
        _animationEvent.AttackEnded -= _attacker.OnCanAttack;
        _animationEvent.DashStart -= _sound.PlayDashSound;

        _inventory.itemAdded -= AddItemToInventory;
        _inventory.itemRemoved -= _inventoryView.Remove;
        
        _dash.Return -= ApplyDamage;
    }

    private void Update()
    {
        _dash.DashTap(_inputReader.GetIsDashTap());
        _isDash = _dash.GetIsDash();

        if (_inputReader.Dirrection != Vector2.zero)
        {
            _lastDirection = _inputReader.Dirrection.normalized;
        }

        _AnimatorController.UpdateAnimationParameters(_inputReader.Dirrection, _isDash, _isAttack = false);
    }

    private void FixedUpdate()
    {
        if (_attacker.canAction)
        {
            if (_inputReader.Dirrection != null && _inputReader.Dirrection != Vector2.zero)
            {
                _mover.Move(_inputReader.Dirrection, _dash.GetIsDash());
                _noiser.CreateNoise();

                if (!_dash.GetIsDash())
                    _sound.PlayStepSound();
            }

            if (_inputReader.GetIsAttack())
            {
                _attacker.UpdateAttackZone(_lastDirection);
                _noiser.CreateNoise();
                _sound.PlayAttackSound();
                _AnimatorController.UpdateAnimationParameters(_inputReader.Dirrection, _isDash, _isAttack = true);
            }

            if (_inputReader.Dirrection == Vector2.zero)
            {
                _mover.Move(Vector2.zero, _dash.GetIsDash());
            }
        }

        else
            _mover.Move(Vector2.zero, _dash.GetIsDash());

        if (_inputReader.GetIsInteract() && _interactable != null)
        {
            if (_interactable.IsLock)
            {
                if (_inventory.Contains(_interactable.Key))
                {
                    _interactable.Unlock((Key)_inventory.Take(_interactable.Key));
                }
                else
                {
                    _interactable.Interact();
                }
            }
            else
            {
                _interactable.Interact();
                _interactableCanvas.gameObject.SetActive(false);
            }
        }
    }

    protected override void OnTakingDamage()
    {
        _sound.PlayHitSound();
        _AnimatorController.UpdateAnimationParameters(_inputReader.Dirrection, _isDash, isHit : true);

        if (_attacker.canAction == false)
            _attacker.OnCanAttack();
    }

    protected override void OnDied()
    {
        base.OnDied();

        _sound.PlayDeathSound();
    }

    private void OnInteractableFounded(IInteractable interactable)
    {
        _interactable = interactable;
        _interactableCanvas.gameObject.SetActive(interactable != null);
    }

    private void OnMedKitFounded(MedKit medKit)
    {
        if (Health.MaxValue > Health.Value) 
        {
            Heal(medKit.Value);
            medKit.Collect();
        }
    }

    private void OnKeyFounded(Key key)
    {
        _inventory.Add(key);
    }

    private void AddItemToInventory(IItem item)
    {
        _inventoryView.Add(item);
        item.Collect();
    }
}