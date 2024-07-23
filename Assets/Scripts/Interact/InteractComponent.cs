using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class InteractComponent : MonoBehaviour
    {
        [SerializeField] private InteractionEvent _action;

        public void Interact(GameObject go) =>
          _action?.Invoke(go);

        [Serializable]
        private class InteractionEvent : UnityEvent<GameObject>
        {
        }


    }
}

