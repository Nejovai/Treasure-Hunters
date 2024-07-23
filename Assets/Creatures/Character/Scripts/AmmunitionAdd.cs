using PixelCrew.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class AmmunitionAdd : MonoBehaviour
    {
        public void Add(GameObject target)
        {
            var ammunitionComponent = target.GetComponent<AmmunitionComponent>();
            if (ammunitionComponent != null)
                ammunitionComponent.Add();

        }

        public void ReloadAll(GameObject target)
        {
            var ammunitionComponent = target.GetComponent<AmmunitionComponent>();
            if (ammunitionComponent != null)
            {
                ammunitionComponent.Reload();
            }
        }
    }
}
