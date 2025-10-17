using UnityEngine;

    [RequireComponent(typeof(InputReader), typeof(PlayerAttacker), typeof(Mover))]
    [RequireComponent(typeof(AnimatorController), typeof(CollisionHandler))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private Health _health;
    private InputReader _inputReader;
    private PlayerAttacker _attacker;
    private Mover _mover;
    private AnimatorController _AnimatorController;
    private CollisionHandler _collisionHandler;
    public bool _isDash;

    private IInteractable _interactable;

    private bool _isAttack;
    private Vector2 _lastDirection;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _inputReader = GetComponent<InputReader>();
        _attacker = GetComponent<PlayerAttacker>();
        _mover = GetComponent<Mover>();
        _AnimatorController = GetComponent<AnimatorController>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        //_isAttack = _inputReader.GetIsAttack();  
        _isDash = _inputReader.GetIsDash();

        if (_inputReader.Dirrection != Vector2.zero)
        {
            _lastDirection = _inputReader.Dirrection.normalized;
        }

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

        if (_inputReader.Dirrection != null)
        {
            _mover.Move(_inputReader.Dirrection, _inputReader.GetIsDash());
        }

        if (_inputReader.GetIsAttack())
        {
                _attacker.Attack(_lastDirection);
                //_AnimatorController.UpdateAnimationParameters(_inputReader.Dirrection, _isAttack = true, _isDash);
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
        print(_health.Value);

        if (_health.Value == 0)
            Destroy(gameObject);
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