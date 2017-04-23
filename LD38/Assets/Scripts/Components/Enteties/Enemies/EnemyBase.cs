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

        public void Damage()
        {
            _animator.SetTrigger(DamageTrigger);
            _speed = _trueSpeed * GamePlayController.Instance.EnemyStop;
            StopAllCoroutines();
            Call(() => _speed = _trueSpeed, GamePlayController.Instance.EnemyStopDuration);
        }
    }
}
