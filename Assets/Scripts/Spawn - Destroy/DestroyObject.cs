using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class DestroyObject : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void Destroy()
        {
            Destroy(_objectToDestroy);
        }
    }
}

