using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        private int _health;
        [SerializeField] private int _maxHealth;

        [Space]
        [Header("Events")]
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent _onHealing;
        [SerializeField] private HealthChangeEvent _onChange;


        private void Start()
        {
            _health = _maxHealth;
        }

        public bool IsHealthy()
        {
            return _health == _maxHealth;
        }

        public void ModifyingHealth(int healthDifferent, int addMaxHealth)
        {
            if (_health <= 0) return;

            if (addMaxHealth != 0)
            {
                _maxHealth += addMaxHealth;
                _health = _maxHealth;
                _onChange?.Invoke(_health, _maxHealth);
            }

            if (healthDifferent != 0)
            {
                _health += healthDifferent;
                if (_health > _maxHealth)
                    _health = _maxHealth;
                _onChange?.Invoke(_health, _maxHealth);
            }

            Debug.Log($"Çהמנמגüו: {_health}");

            if (healthDifferent > 0)
                _onHealing?.Invoke();

            if (healthDifferent < 0)
                _onDamage?.Invoke();

            if (_health <= 0)
                _onDie?.Invoke();
        }

        public void SetHealth(int health, int maxHealth)
        {
            _health = health;
            _maxHealth = maxHealth;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int, int>
        {
        }
    }
}

