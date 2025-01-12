using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using System;
using UnityEngine;

namespace PixelCrew.Model
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public IntProperty Hp = new IntProperty();
        public IntProperty MaxHp = new IntProperty();
        public PerksData Perks = new PerksData();

        public InventoryData Inventory =>_inventory;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
