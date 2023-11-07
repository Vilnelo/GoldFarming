using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private float _damageJump;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _items;
    [SerializeField] private LayerMask _spikes;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    //private static readonly int Is_Ground_Key = Animator.StringToHash("Is_ground");
    //private static readonly int is_running_Key = Animator.StringToHash("is_running");
    //private static readonly int vertical_velocity_Key = Animator.StringToHash("vertical_velocity");
    //private static readonly int is_hit_Key = Animator.StringToHash("is_hit");

    private Rigidbody2D _rigidbody;

    private Vector2 _direction;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private string _currentAnimation;
    private bool _isGrounded;
    private bool _allawDoubleJump;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;

    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
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

        
        //_animator.SetBool(Is_Ground_Key, isGrounded);
        //_animator.SetBool(is_running_Key, _direction.x != 0);
        //_animator.SetFloat(vertical_velocity_Key, _rigidbody.velocity.y);
        //_animator.SetBool(is_hit_Key, isSpikes);

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

        if (_isGrounded) _allawDoubleJump = true;
        if (isJumping)
        {
            _yVelosity = CalculateJumpVelocity(_yVelosity);
        }
        else if (_rigidbody.velocity.y > 0)

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
            _sprite.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _sprite.flipX = true;
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
        SetClip("hit");
        if (!_isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJump);
        } 
       

    }
}
