using System;
using Assets.Scripts.Controllers;
using Assets.Scripts.Core.Staff;
using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;

namespace Assets.Scripts.Components.BulletSystem
{
    class BulletSpawner : BindingMonoBehaviour
    {
        [SerializeField] private float _scatter;
        [SerializeField] private float _delay;
        [SerializeField] private float _speed;

        [SerializeField] private float _time;

        private bool TimeCompleted { get { return _time + _delay < Time.time; } }
        
        public void Spawn(Vector2 position, Vector2 dir)
        {
            if (!TimeCompleted) return;
            _time = Time.time;
            var bullet = PoolsHandlerController.Instance.ShipBullets.Pop<Bullet>();
            bullet.Init(position, dir, _speed);
        }
    }
}
