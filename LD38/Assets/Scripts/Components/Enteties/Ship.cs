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

        [SerializeField] private int _health;
        [SerializeField] private int _currentHealth;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] [Binding(true)] private Animator _animator;

        private bool TimeToMakeShot { get { return InputController.Instance.Shot; } }

        protected virtual void Start()
        {
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
            if (!other.collider.tag.Equals(EnemyBase.Tag)) return;
            EffectController.Instance.BigShake();
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
        }
    }
}
