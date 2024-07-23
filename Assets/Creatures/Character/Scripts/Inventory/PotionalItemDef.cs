using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/PotionalItemDefs", fileName = "PotionalItemDefs")]
    public class PotionalItemDef : ScriptableObject
    {
        [SerializeField] private PotionalDef[] _items;

        public PotionalDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }
            return default;
        }
    }

    [Serializable]
    public struct PotionalDef
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _value;

        public string Id => _id;
        public int Value => _value;
    }
}
