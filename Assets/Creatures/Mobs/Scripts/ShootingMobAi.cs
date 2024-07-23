using PixelCrew.Components;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class ShootingMobAi : ShootingAI
    {
        [Header("Components")]
        [SerializeField] private LayerCheck _vision;

        [Header("Melee Attack")]
        [SerializeField] private bool _canMeleeAttack;
        [SerializeField] private CheckCircleOverlap _meleeAttack;       
        [SerializeField] private LayerCheck _meleeCanAttack;
        [SerializeField] private Cooldown _meleeCooldown;
        

        //ANIMATOR
        private static readonly int Melee = Animator.StringToHash("Melee");


        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_rangeCooldown.IsReady)
                    RangeAttack();

                if (_canMeleeAttack && _meleeCanAttack.IsTouchingLayer)
                {
                    if (_meleeCooldown.IsReady)
                        MeleeAttack();
                    return;
                }
            }

            
                
        }



       /* private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_meleeCanAttack.IsTouchingLayer)
                {
                    if (_meleeCooldown.IsReady)
                        MeleeAttack();
                    return;
                }

                if (_rangeCooldown.IsReady)
                    RangeAttack();
            }
        } */

        private void MeleeAttack()
        {
            _animator.SetTrigger(Melee);
            _meleeCooldown.Reset();
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }
    }
}
