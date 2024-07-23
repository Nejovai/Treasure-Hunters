using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDestroy;

    private void Start()
    {
        Invoke("Destroy", 2);
    }

    private void Destroy()
    {
        Destroy(_objectToDestroy);
    }
}
