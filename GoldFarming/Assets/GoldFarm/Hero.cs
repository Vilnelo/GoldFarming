using GoldFarm.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private float _damageJump;
    [SerializeField] private float _isHightJump;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _items;
    [SerializeField] private LayerMask _spikes;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private SpawnComponent _footStepParticles;
    [SerializeField] private ParticleSystem _jumpParticle;
    [SerializeField] private ParticleSystem _fallParticle;


    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    private Rigidbody2D _rigidbody;
    private float _startHightPosition;
    private Collider2D[] _interactionResult = new Collider2D[1];

    private Vector2 _direction;
    private Animator _animator;
    private string _currentAnimation;
    private bool _isGrounded;
    private bool _allawDoubleJump;
    private bool _isJumpingPressed;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;

    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _startHightPosition = _rigidbody.position.y;
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

        if (_direction.x != 0 && _isGrounded)
        {
            SetClip("run");
        }
        else if (_rigidbody.velocity.y > 0 && !_isGrounded)
        {
            SetClip("jump");
        } else if (_rigidbody.velocity.y < 0 && !_isGrounded)
        {
            SetClip("fall");
        } else
        {
            SetClip("idle");
        }

        UpdateSpriteDirection();

    }

    private float CalculateYVelocity()
    {
        var _yVelosity = _rigidbody.velocity.y;
        var isJumping = _direction.y > 0;

        if (_isGrounded)
        {
            _isJumpingPressed = false;
            _allawDoubleJump = true;
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
            _yVelosity += _jump;
        }
        else if (_allawDoubleJump)
        {
            _yVelosity = _jump;
            _allawDoubleJump = false;
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
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _ground);
        return hit.collider != null;
    }

    private bool IsItems()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _items);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : IsItems() ? Color.yellow : Color.red;
        Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
    }

    private void SetClip(string _animation)
    {
        if (_currentAnimation == _animation) return;

        _animator.Play(_animation);
        _currentAnimation = _animation;
    }

    public void TakeDamage() 
    {
        _isJumpingPressed = false;
        SetClip("hit");
        if (!_isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJump);
        }
    }

    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _items);

        for (int i = 0; i < size; i ++) {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void SpawnFootDust()
    {
        _footStepParticles.Spawn();
    }

    public void SpawnJumpDust()
    {
        _jumpParticle.gameObject.SetActive(true);
        _jumpParticle.Play();
    }

    public void IsHight()
    {
        _startHightPosition = _rigidbody.position.y;
    }
    public void SpawnFallDust()
    {
        if (_startHightPosition - _isHightJump > _rigidbody.position.y)
        {
            _fallParticle.gameObject.SetActive(true);
            _fallParticle.Play();
            _startHightPosition = _rigidbody.position.y;
        }
    }
}
