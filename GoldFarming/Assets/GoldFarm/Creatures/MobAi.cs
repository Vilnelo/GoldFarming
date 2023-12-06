using GoldFarm.Components;
using System.Collections;
using UnityEngine;

namespace GoldFarm.Creatures
{
    public class MobAi : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.2f;
        [SerializeField] private float _attackCooldown = 0.2f;
        [SerializeField] private float _missHeroCooldown = 0.2f;

        private Coroutine _current;
        private GameObject _target;
        //private CapsuleCollider2D _capsuleCollider;

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private Patrol _patrol;

        private static readonly int DieKey = Animator.StringToHash("die");
        private bool _isDead;


        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
            //_capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;

            _target = go;

            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {

            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }

            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missHeroCooldown);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            StartState(GoToHero() );
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);

            if (_current != null) StopCoroutine(_current);

            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _animator.SetBool(DieKey, true);
            _isDead = true;
            _creature.SetDirection(Vector2.zero);
            //_capsuleCollider.direction = CapsuleDirection2D.Horizontal;

            //_capsuleCollider.size = new Vector2(_capsuleCollider.size.y, _capsuleCollider.size.x);
            //_capsuleCollider.offset = new Vector2(_capsuleCollider.offset.x, _capsuleCollider.offset.y);

            if (_current != null)
            {
                StopCoroutine(_current);
            }
        }
    }
}