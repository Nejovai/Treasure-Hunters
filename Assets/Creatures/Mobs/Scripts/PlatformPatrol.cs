using PixelCrew.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private LayerCheck _platformCheck;
        [SerializeField] private LayerCheck _wallCheck;

        private Creature _creature;
        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }
        public override IEnumerator DoPatrol()
        {
            Vector2 direction = transform.right;
            _creature.SetDirection(direction);

            while (enabled)
            {
                if (!IsPlatform() || IsWall())
                {
                    direction.x *= -1;
                    _creature.SetDirection(direction);
                }
                
                yield return null;
            }         
        }

        private bool IsPlatform()
        {
            return _platformCheck.IsTouchingLayer;
        }

        private bool IsWall()
        {
            return _wallCheck.IsTouchingLayer;
        }

    }
}

