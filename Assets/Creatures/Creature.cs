using PixelCrew.Components;
using PixelCrew.Components.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        //Parameters
        [Header("Parameters")]
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpStreight;
        [SerializeField] private float _damageJumpStreight;

        //Components
        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected bool IsGrounded;
        protected PlaySoundComponent Sounds;
        [Header("Components")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        //Animator
        protected Animator Animator;
        private static readonly int IsGroundKey = Animator.StringToHash("IsGround");
        private static readonly int IsMovingKey = Animator.StringToHash("IsMoving");
        private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attacking = Animator.StringToHash("Attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundComponent>();
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var velocityX = Direction.x * _speed;
            var velocityY = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(velocityX, velocityY);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(IsMovingKey, Direction.x != 0);

            Flip(Direction);
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void Flip(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }

        protected virtual float CalculateYVelocity()
        {
            var velocityY = Rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (isJumpPressing)
            {
                var isFalling = Rigidbody.velocity.y <= 0.001f;
                if (!isFalling)
                    return velocityY;
                velocityY = isFalling ? CalculateJumpVelocity(velocityY) : velocityY;

            }

            else if (Rigidbody.velocity.y > 0)
                velocityY *= 0.5f;

            return velocityY;
        }

        protected virtual float CalculateJumpVelocity(float velocityY)
        {
            if (IsGrounded)
            {
                velocityY = _jumpStreight;
                DoJumpVfx();
            }

            return velocityY;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");
            Sounds.Play("Jump");
        }

        public virtual void TakeDamage()
        {
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageJumpStreight);
        }

        public virtual void Attack()
        {
            if (IsGrounded)
            {
                Animator.SetTrigger(Attacking);
                Sounds.Play("Melee");
            }
                
        }

        public void OnAttacking()
        {
            _attackRange.Check();
            _particles.Spawn("Attack");
        }

    }
}