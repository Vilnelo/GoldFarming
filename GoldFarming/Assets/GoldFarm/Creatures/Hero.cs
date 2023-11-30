using GoldFarm.Components;
using GoldFarm.Model;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace GoldFarm.Creatures {
    public class Hero : Creature
    {       
        [SerializeField] private float _fallVelocity;
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [SerializeField] private LayerMask _spikes;

        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private Vector3 _groundCheckPositionDelta;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disArmed;

        private bool _allowDoubleJump;
        private bool _isOnWall;
        private float _defaultGravityScale;
        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }


        protected override void Update()
        {
            base.Update();

            if (_wallCheck.IsTouchingLayer && Direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }

        }


        protected override float CalculateYVelocity()
        {
            var _yVelosity = Rigidbody.velocity.y;
            var isJumping = Direction.y > 0;

            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }
            if (!IsJumpingPressed && _isOnWall)
            {
                return 0f;
            }
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float _yVelosity)
        {
            if (!IsGrounded && _allowDoubleJump)
            {
                SpawnParticle(JumpParticle);
                _allowDoubleJump = false;
                return JumpSpeed;
            }
            return base.CalculateJumpVelocity(_yVelosity);

        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(GroundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _fallVelocity)
                {
                    SpawnParticle(FallParticle);
                }
            }
        }

        public override void Attack()
        {
            if (!_session.Data.IsArmed) return;
            base.Attack();
            SpawnParticle(SwordParticle);
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disArmed;
        }
    }
}