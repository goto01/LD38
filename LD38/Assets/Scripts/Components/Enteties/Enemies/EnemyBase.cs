using System.Collections;
using Assets.Scripts.Components.Movement;
using Assets.Scripts.Controllers;
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

        protected override void Awake()
        {
            base.Awake();
            _currentLife = _lifes;
        }

        public void Damage()
        {
            if (--_currentLife == 0)
            {
                _currentLife = _lifes;
                gameObject.SetActive(false);
                var explosion = EffectController.Instance.BigExplosions.Pop();
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y,
                    explosion.transform.position.z);
                _speed = _trueSpeed;
                return;
            }
            _animator.SetTrigger(DamageTrigger);
            _speed = _trueSpeed * GamePlayController.Instance.EnemyStop;
            StopAllCoroutines();
            Call(() => _speed = _trueSpeed, GamePlayController.Instance.EnemyStopDuration);
        }
    }
}
