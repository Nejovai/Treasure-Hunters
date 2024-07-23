using PixelCrew.Components;
using PixelCrew.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class ShootingAI : MonoBehaviour
    {
        protected Animator _animator;

        [Header("Range Attack")]
        [SerializeField] protected SpawnComponent _rangeAttack;
        [SerializeField] protected Cooldown _rangeCooldown;

        //ANIMATOR
        private static readonly int Range = Animator.StringToHash("Range");

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void RangeAttack()
        {
            _animator.SetTrigger(Range);
            _rangeCooldown.Reset();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}
