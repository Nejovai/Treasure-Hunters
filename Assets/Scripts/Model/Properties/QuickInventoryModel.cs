﻿using PixelCrew.Definitions;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Utils.Disposables;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PixelCrew.Model.Properties
{
    public class QuickInventoryModel : IDisposable
    {
        private PlayerData _data;
        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();

        public event Action OnChanged;

        public InventoryItemData SelectedItem => Inventory[SelectedIndex.Value];

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public QuickInventoryModel(PlayerData data)
        {
            _data = data;
            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            _data.Inventory.OnChanged += OnChangedInventory;
        }

        private void OnChangedInventory(string id, int value)
        {
            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();

        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }

        public void Dispose()
        {
            _data.Inventory.OnChanged -= OnChangedInventory;
        }
    }
}
