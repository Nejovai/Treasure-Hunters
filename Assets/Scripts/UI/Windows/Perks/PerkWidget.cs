﻿using PixelCrew.Model;
using PixelCrew.Model.Definitions.Repositories;
using PixelCrew.UI.Widgets;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Perks
{
    public class PerkWidget : MonoBehaviour, IItemRenderer<PerkDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isLocked;
        [SerializeField] private GameObject _isUsed;
        [SerializeField] private GameObject _isSelected;

        private GameSession _session;
        private PerkDef _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(PerkDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        public void OnSelect()
        {
            _session.PerksModel.InterfaceSelection.Value = _data.Id;
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _isLocked.SetActive(!_session.PerksModel.IsUnlocked(_data.Id));
            _isSelected.SetActive(_session.PerksModel.InterfaceSelection.Value == _data.Id);
            _isUsed.SetActive(_session.PerksModel.IsUsed(_data.Id));
        }
    }
}
