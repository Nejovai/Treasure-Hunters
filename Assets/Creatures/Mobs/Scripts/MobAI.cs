using PixelCrew.Components;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _missCooldown = 2f;

        [SerializeField] private bool _directionCollider=true;
        [SerializeField] private Vector2 _deadColliderSize;
        private Coroutine _current;
        private GameObject _target;
        private Patrol _patrol;

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private bool _isDead;
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnCharacterInVision(GameObject gameObject)
        {
            if (_isDead) return;

            _target = gameObject;

            StartState(AgroToCharacter());
        }

        private IEnumerator AgroToCharacter()
        {
            LookAtCharacter();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            StartState(GoToCharacter());
        }

        private IEnumerator GoToCharacter()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }                
                else
                    SetDirectionToTarget();
                
                yield return null;
            }

            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missCooldown);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            StartState(GoToCharacter());
                
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }

        private void LookAtCharacter()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.Flip(direction);
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

            if (_current!=null)
                StopCoroutine(_current);
            
            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDead, true);
            _creature.SetDirection(Vector2.zero);

            if (_current!=null)
                StopCoroutine(_current);

            if (_directionCollider)
                GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
            
            GetComponent<CapsuleCollider2D>().size = _deadColliderSize;
            gameObject.layer = LayerMask.NameToLayer("Trash");
        }
    }
}

