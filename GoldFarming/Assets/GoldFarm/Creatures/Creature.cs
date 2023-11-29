using GoldFarm.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldFarm.Creatures
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] protected float JumpSpeed;
        [SerializeField] protected float DamageJumpSpeed;
        [SerializeField] private int _damage;
        [SerializeField] protected LayerMask GroundLayer;

        [SerializeField] private LayerCheck _groundCheck;

        [SerializeField] private CheckCircleOverlap _attackRange;

        [Space]
        [Header("Particle")]
        [SerializeField] private SpawnListComponent _particles;
        [SerializeField] protected ParticleSystem JumpParticle;
        [SerializeField] protected ParticleSystem FallParticle;
        [SerializeField] protected ParticleSystem SwordParticle;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected bool IsGrounded;
        protected bool IsJumpingPressed;

        private static readonly int IsGroundKey = Animator.StringToHash("is_ground");
        private static readonly int IsRunning = Animator.StringToHash("is_running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical_velocity");
        private static readonly int Hit = Animator.StringToHash("is_hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update() 
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetBool(IsRunning, Direction.x != 0);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);

            UpdateSpriteDirection();

        }
        protected virtual float CalculateYVelocity()
        {
            var yVelosity = Rigidbody.velocity.y;
            var isJumping = Direction.y > 0;

            if (IsGrounded)
            {
                IsJumpingPressed = false;
            }
            if (isJumping)
            {
                IsJumpingPressed = true;

                var _isFalling = Rigidbody.velocity.y <= 0.001f;
                yVelosity = _isFalling ? CalculateJumpVelocity(yVelosity) : yVelosity;
            }
            else if (Rigidbody.velocity.y > 0 && IsJumpingPressed)

            {
                yVelosity *= 0.5f;

            }

            return yVelosity;
        }

        protected virtual float CalculateJumpVelocity(float _yVelosity)
        {
            if (IsGrounded)
            {
                _yVelosity += JumpSpeed;
                SpawnParticle(JumpParticle);
            }
            return _yVelosity;

        }

        public void SpawnParticle(ParticleSystem particles)
        {
            particles.gameObject.SetActive(true);
            particles.transform.localScale = transform.lossyScale;
            particles.Play();
        }

        private void UpdateSpriteDirection()
        {

            if (Direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        public void TakeDamage()
        {
            IsJumpingPressed = false;
            Animator.SetTrigger(Hit);
            if (!IsGrounded)
            {
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, DamageJumpSpeed);
            }
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
            SpawnParticle(SwordParticle);
        }
        public void OnDoAttack()
        {
            _attackRange.Check();
        }
    }
}

