using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ModifyHealth : MonoBehaviour
    {
        [SerializeField] private int _modifyHealth;
        [SerializeField] private int _modifyMaxHealth;
        [SerializeField] private GameObject _prefab;

        public void Apply(GameObject target)
        {
            var healthComponent=target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyingHealth(_modifyHealth, _modifyMaxHealth);
            }
        }
    }
}

