using GoldFarm;
using GoldFarm.Components;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _damageJumpSpeed;
    [SerializeField] private float _fallVelocity;
    [SerializeField] private float _interactionRadius;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _itemsLayer;
    [SerializeField] private LayerMask _spikes;

    [Space][Header("Particle")]
    [SerializeField] private SpawnComponent _footStepParticles;
    [SerializeField] private ParticleSystem _jumpParticle;
    [SerializeField] private ParticleSystem _fallParticle;


    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    private Collider2D[] _interactionResult = new Collider2D[1];
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private string _currentAnimation;
    private bool _isGrounded;
    private bool _allowDoubleJump;
    private bool _isJumpingPressed;

    private static readonly int IsGroundKey = Animator.StringToHash("is_ground");
    private static readonly int IsRunning = Animator.StringToHash("is_running");
    private static readonly int VerticalVelocity = Animator.StringToHash("vertical_velocity");
    private static readonly int Hit = Animator.StringToHash("is_hit");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;

    }

    private void Update()
    {
        _isGrounded = IsGrounded() || IsItems();

    }

    public void FixedUpdate()
    {
        var xVelocity = _direction.x * _speed;
        var yVelocity = CalculateYVelocity();
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        _animator.SetBool(IsGroundKey, _isGrounded);
        _animator.SetBool(IsRunning, _direction.x != 0);
        _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);

        UpdateSpriteDirection();

    }

    private float CalculateYVelocity()
    {
        var _yVelosity = _rigidbody.velocity.y;
        var isJumping = _direction.y > 0;

        if (_isGrounded)
        {
            _isJumpingPressed = false;
            _allowDoubleJump = true;
        }
        if (isJumping)
        {
            _isJumpingPressed = true;
            _yVelosity = CalculateJumpVelocity(_yVelosity);
        }
        else if (_rigidbody.velocity.y > 0 && _isJumpingPressed)

        {
            _yVelosity *= 0.5f;

        }

        return _yVelosity;
    }

    private float CalculateJumpVelocity(float _yVelosity)
    {
        var _isFalling = _rigidbody.velocity.y <= 0.001f;
        if (!_isFalling) return _yVelosity;
        if (_isGrounded || IsItems())
        {
            _yVelosity += _jumpSpeed;
            SpawnJumpDust();
        }
        else if (_allowDoubleJump)
        {
            SpawnJumpDust();
            _yVelosity = _jumpSpeed;
            _allowDoubleJump = false;
        }
        return _yVelosity;

    }
    private void UpdateSpriteDirection()
    {

        if (_direction.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _groundLayer);
        return hit.collider != null;
    }

    private bool IsItems()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _itemsLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : IsItems() ? Color.yellow : Color.red;
        Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
    }

    public void TakeDamage() 
    {
        _isJumpingPressed = false;
        _animator.SetTrigger(Hit);
        if (!_isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);
        }

    }

    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _itemsLayer);

        for (int i = 0; i < size; i ++) {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void SpawnJumpDust()
    {
        _jumpParticle.gameObject.SetActive(true);
        _jumpParticle.Play();
    }

    public void FallDust()
    {
        _fallParticle.gameObject.SetActive(true); 
        _fallParticle.Play();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.IsInLayer(_groundLayer))
        {
            var contact = other.contacts[0];
            if (contact.relativeVelocity.y >= _fallVelocity)
            {
                FallDust();
            }
        }
    }

    public void SpawnFootDust()
    {
        _footStepParticles.Spawn();
    }
}
