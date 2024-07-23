using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class SinusoidProjectile : BaseProjectile
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private float _offsetY;
        [SerializeField] private bool _reboundGround;
        private float _originalY;
        private float _time;
        protected override void Start()
        {
            base.Start();
            _originalY = Rigidbody.position.y +_offsetY;
        }

        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            position.x += Direction * _speed;
            var sinusoid = Mathf.Sin(_time * _frequency) * _amplitude;
            if (_reboundGround)
                position.y = _originalY + Mathf.Abs(sinusoid);
            else
                position.y = _originalY + sinusoid;
            
            Rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }

    }
}