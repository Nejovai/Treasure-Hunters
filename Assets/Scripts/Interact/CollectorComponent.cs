using PixelCrew.Model;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        [SerializeField] private List<InventoryItemData> _items = new List<InventoryItemData>();
        public void AddInInventory(string id, int value)
        {
            _items.Add(new InventoryItemData (id) {Value = value });
        }

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();
            if (session != null)
            {
                foreach (var inventoryItemData in _items)
                {
                    session.Data.Inventory.Add(inventoryItemData.Id, inventoryItemData.Value);
                }

                _items.Clear();
            }
        }
    }
}
