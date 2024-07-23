using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.Movement
{
    public class CircleLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        private Rigidbody2D _rb;
        private Vector2 _center;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _center = transform.position;

        }
        private void Update()
        {
            var pos = _rb.position;
            pos.x = _center.x + _radius * Mathf.Sin(Mathf.PI * Time.time);
            pos.y = _center.y + _radius * Mathf.Cos(Mathf.PI * Time.time);
            _rb.MovePosition(pos);
        }

    }
}
