using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _invertScale=true;

        public void Spawn()
        {
            var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);

            if (_invertScale)
                instantiate.transform.localScale = _target.lossyScale;

            else return;
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    }
}

