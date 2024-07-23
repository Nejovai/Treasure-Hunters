using UnityEngine;
using PixelCrew.Components;
using UnityEditor.Animations;
using PixelCrew.Model;
using System;
using PixelCrew.Utils;
using PixelCrew.Model.Definitions;
using PixelCrew.Definitions;

namespace PixelCrew.Creatures
{
    public class Character : Creature, ICanAddInInventory
    {
        //CHARACTER PARAMETERS
        [Header("Player Parameters")]
        [SerializeField] private float _dashStreight;

        //COMPONENTS       
        private bool _allowDoubleJump;
        private GameSession _session;
        [Header("Player Components")]
        [SerializeField] private CheckCircleOverlap _interationCheck;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private float _slamDownVelocity;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;
        [SerializeField] private Cooldown _dashCooldown;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private SpawnComponent _throwSpawner;
        private HealthComponent _health;
        private AmmunitionComponent _ammunition;

        //ANIMATOR
        private static readonly int Throwing = Animator.StringToHash("Throw");

        //PARTICLE COMPONENT
        [Header("Player Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        private const string SwordId = "Sword";
        private int SwordCount => _session.Data.Inventory.Count(SwordId);
        private int CoinCount => _session.Data.Inventory.Count("Coin");

        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId)
                    return SwordCount > 1;
                
                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        private bool CanHealing
        {
            get
            {
                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Usable);
            }
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _ammunition = GetComponent<AmmunitionComponent>();
            _health = GetComponent<HealthComponent>();
            var maxHealth = GetComponent<HealthComponent>();
            _health.SetHealth(_session.Data.Hp.Value, _session.Data.MaxHp.Value);
            maxHealth.SetHealth(_session.Data.Hp.Value, _session.Data.MaxHp.Value);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId)
                UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth, int currentMaxHealth)
        {
            _session.Data.Hp.Value = currentHealth;
            _session.Data.MaxHp.Value = currentMaxHealth;
        }

        protected override float CalculateYVelocity()
        {
            if (IsGrounded)
                _allowDoubleJump = true;

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float velocityY)
        {
            if (!IsGrounded && _allowDoubleJump && _session.PerksModel.IsDoubleJumpSupported)
            {
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpStreight;
            }
            return base.CalculateJumpVelocity(velocityY);
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        public override void Attack()
        {
            if (SwordCount <= 0) return;

            base.Attack();
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinCount > 0)
                SpawnCoins();
        }

        public void Interact()
        {
            _interationCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(_groundLayer))
            {
                var contact = collision.contacts[0];
                if (!_allowDoubleJump || contact.relativeVelocity.y >= _slamDownVelocity)
                    _particles.Spawn("Landing");
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Dash()
        {
            if (_dashCooldown.IsReady && !IsGrounded && _session.PerksModel.IsDashSupported)
            {
                _particles.Spawn("Dash");
                Rigidbody.velocity = new Vector2(0, 0);
                Rigidbody.AddForce(new Vector2(transform.localScale.x, 0) * _dashStreight);
                _dashCooldown.Reset();
            }
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        }

        public void OnThrowing()
        {
            Sounds.Play("Range");
            var throwableId = _session.QuickInventory.SelectedItem.Id;
            var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();
        }

        public void UseFromInventory()
        {
            string selectedItemId = _session.QuickInventory.SelectedItem.Id;
            ItemDef itemDef = DefsFacade.I.Items.Get(selectedItemId);
            if (itemDef.HasTag(ItemTag.Potion))
                UsePotion(selectedItemId);
            
            if (itemDef.HasTag(ItemTag.Throwable))
                Throw(selectedItemId);
            
            _session.Data.Inventory.Remove(selectedItemId, 1);
        }

        private void Throw(string throwableId)
        {
            const string swordId = SwordId;
            int swordCount = _session.Data.Inventory.Count(swordId);

            if (_throwCooldown.IsReady && CanThrow)
            {
                Animator.SetTrigger(Throwing);
                _throwCooldown.Reset();
            }
        }

        private void UsePotion(string potionId)
        {
            PotionalDef potionDef = DefsFacade.I.Potional.Get(potionId);
            _health.ModifyingHealth(potionDef.Value, 0);
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }
    }
}

