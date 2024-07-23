using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PixelCrew.Components
{
    public class DoInteractionComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _interactor;

        public void DoInteraction(GameObject gameObject)
        {
            var interactable = gameObject.GetComponent<InteractComponent>();
            if (interactable != null)
                interactable.Interact(_interactor);
        }
    }
}

