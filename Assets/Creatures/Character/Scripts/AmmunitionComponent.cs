using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class AmmunitionComponent : MonoBehaviour
    {
        [SerializeField] private int _maxCount;
        [SerializeField] private int _count;
        [SerializeField] private AmmunitionChangeEvent _onChanged;

        private void Start()
        {
            _count = _maxCount;
        }

        public void Use()
        {
            _count--;
            _onChanged?.Invoke(_count, _maxCount);
        }

        public void Add()
        {
            if (_count<_maxCount)
                _count++;
            _onChanged?.Invoke(_count, _maxCount);
        }

        public bool Any()
        {
            return _count > 1;
        }

        public void Reload()
        {
            _count = _maxCount;
            _onChanged?.Invoke(_count, _maxCount);
        }

        public void SetAmmunition(int ammunition, int maxAmmunition)
        {
            _count = ammunition;
            _maxCount = maxAmmunition;
        }

        [Serializable]
        public class AmmunitionChangeEvent : UnityEvent<int, int>
        {
        }
    }
}
