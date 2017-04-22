using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers;
using Assets.Scripts.Core.PropertyAttributes;
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
        [Binding(true)] [SerializeField] private Animator _animator;

        private Dictionary<float, float> _angle = new Dictionary<float, float>()
        {
            { .11f, 20 },
            { .21f, 10f },
            { .31f, 5f },
        }; 

        private bool TimeCompleted { get { return _time + _delay < Time.time; } }
        
        public void Spawn(Vector2 position, Vector2 dir)
        {
            var angle = 0f;
            for (var index = 0; index < _angle.Keys.Count; index++)
            {
                var item = _angle.ElementAt(index);
                angle = item.Value;
                if (_delay < item.Key) break;
            }
            dir = Quaternion.Euler(0, 0, Random.Range(-angle, angle))*dir;

            if (!TimeCompleted) return;
            _time = Time.time;
            var bullet = PoolsHandlerController.Instance.ShipBullets.Pop<Bullet>();
            bullet.Init(position, dir, _speed);
            _animator.SetTrigger("Shot");
        }
    }
}
