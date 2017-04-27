using Assets.Scripts.Components.BulletSystem;
using Assets.Scripts.Components.Enteties.Enemies;
using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EffectController;
using Assets.Scripts.Controllers.InputController;
using Assets.Scripts.Core.PropertyAttributes;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Enteties
{
    class Ship : BindingMonoBehaviour
    {
        private readonly int DamageTrigger = Animator.StringToHash("Damage");
        private readonly int InputDisabled = Animator.StringToHash("Input disabled");

        public const string Tag = "Character";

        [SerializeField] private int _health;
        [SerializeField] private int _currentHealth;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] [Binding(true)] private Animator _animator;
        private bool _damageEnable = true;
        private Vector3 _origin;

        private bool TimeToMakeShot { get { return InputController.Instance.Shot; } }
        public int CurrentHealth { get { return _currentHealth; } }

        protected virtual void OnEnable()
        {
            _damageEnable = true;
        }

        protected virtual void Start()
        {
            _origin = transform.position;
            ResetSelf();
        }

        protected virtual void Update()
        {
            _animator.SetBool(InputDisabled, InputController.Instance.Disabled);
            UIController.Instance.ShipHealth = _currentHealth;
            if (TimeToMakeShot) MakeShot();
            var pos = (Vector3)InputController.Instance.GetDirectionToPointer(transform.position);
            var angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        }

        private void MakeShot()
        {
            _bulletSpawner.Spawn(transform.position, InputController.Instance.GetDirectionToPointer(transform.position));
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.tag.Equals(EnemyBase.Tag) && !other.collider.tag.Equals(EemyBullet.Tag)) return;
            Damage();
        }

        public void Damage()
        {
            if (!_damageEnable) return;
            _damageEnable = false;
            Call(() => _damageEnable = true, .5f);
            EffectController.Instance.BigShake();
            var ex = EffectController.Instance.BigExplosions.Pop();
            ex.transform.position = new Vector3(transform.position.x, transform.position.y, ex.transform.position.z);
            _currentHealth--;
            DamageAnimator();
        }

        private void DamageAnimator()
        {
            _animator.SetTrigger(DamageTrigger);
        }

        public void ResetSelf()
        {
            _currentHealth = _health;
            transform.position = _origin;
            GamePlayController.Instance.ResetKilledEnemies();
        }

        public void IncHealth()
        {
            _currentHealth = Mathf.Clamp(++_currentHealth, 0, _health);
        }
    }
}
