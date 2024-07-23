using PixelCrew.Components;
using PixelCrew.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class InspectionPatrol : Patrol
    {
        [SerializeField] private Cooldown _turn;
        private Creature _creature;
        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            Vector2 direction = transform.right;
            while (enabled)
            {
                if (_turn.IsReady)
                {
                    direction *= -1;
                    _creature.Flip(direction);
                    _turn.Reset();
                }
                yield return null;
            }
        }
    }
}

