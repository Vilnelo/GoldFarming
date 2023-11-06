using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _items;
    [SerializeField] private LayerMask _spikes;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    private static readonly int Is_Ground_Key = Animator.StringToHash("Is_ground");
    private static readonly int is_running_Key = Animator.StringToHash("is_running");
    private static readonly int vertical_velocity_Key = Animator.StringToHash("vertical_velocity");
    private static readonly int is_hit_Key = Animator.StringToHash("is_hit");

    private Rigidbody2D _rigidbody;

    private Vector2 _direction;
    private Animator _animator;
    private SpriteRenderer _sprite;

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


    public void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        var isJumping = _direction.y > 0;
        var isGrounded = IsGrounded() || IsItems();
        var isSpikes = IsSpikes();

        _animator.SetBool(Is_Ground_Key, isGrounded);
        _animator.SetBool(is_running_Key, _direction.x != 0);
        _animator.SetFloat(vertical_velocity_Key, _rigidbody.velocity.y);
        _animator.SetBool(is_hit_Key, isSpikes);

        UpdateSpriteDirection();

        if (isJumping)
        {
            if (isGrounded && _rigidbody.velocity.y <= 0)
            {
                _rigidbody.AddForce(Vector2.up * _jump, ForceMode2D.Impulse);

            }
            else if (IsItems() && !isGrounded && _rigidbody.velocity.y <= 0)
            {
                _rigidbody.AddForce(Vector2.up * _jump, ForceMode2D.Impulse);

            }

        }
        else if (_rigidbody.velocity.y > 0)

        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);


        }
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

    private bool IsSpikes()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _spikes);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : IsItems() ? Color.yellow : Color.red;
        Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
    }


}
