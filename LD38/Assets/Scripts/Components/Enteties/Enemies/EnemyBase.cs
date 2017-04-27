using System.Collections;
using Assets.Scripts.Components.Movement;
using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EffectController;
using Assets.Scripts.Core.PropertyAttributes;
using UnityEngine;

namespace Assets.Scripts.Components.Enteties.Enemies
{
    public abstract class EnemyBase : BaseMovement, IEntity
    {
        private static readonly int DamageTrigger = Animator.StringToHash("Damage");
        public const string Tag = "Enemy";

        [Binding(true)] [SerializeField] protected Animator _animator;
        [SerializeField] private int _lifes;
        [SerializeField] private int _currentLife;
        [SerializeField] private bool _activated = false;

        protected override void Awake()
        {
            base.Awake();
            _currentLife = _lifes;
        }

        protected override void Translate()
        {
            if (_activated) base.Translate();
        }

        public void Damage()
        {
            if (!_activated) return;
            if ((_currentLife-=2) <= 0)
            {
                Die();
                return;
            }
            _animator.SetTrigger(DamageTrigger);
            _speed = _trueSpeed * GamePlayController.Instance.EnemyStop;
            StopAllCoroutines();
            Call(() => _speed = _trueSpeed, GamePlayController.Instance.EnemyStopDuration);
        }

        public virtual void Activate()
        {
            _activated = true;
            gameObject.SetActive(true);
        }

        protected virtual void Die()
        {
            AnamlyticsController.Instance.SendEnemyKilled();
            _currentLife = _lifes;
            gameObject.SetActive(false);
            var explosion = EffectController.Instance.BigExplosions.Pop();
            EffectController.Instance.BigShake();
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y,
                explosion.transform.position.z);
            _speed = _trueSpeed;
        }
    }
}
