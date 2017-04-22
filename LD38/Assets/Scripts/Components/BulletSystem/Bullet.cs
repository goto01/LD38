using Assets.Scripts.Components.Movement;
using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;

namespace Assets.Scripts.Components.BulletSystem
{
    enum BulletType
    {
        Simple,
    }

    class Bullet : BaseMovement
    {
        [SerializeField] private Vector2 _direction;

        protected override Vector2 Direction
        {
            get { return _direction; }
        }

        public void Init(Vector2 position, Vector2 direction)
        {
            var pos = transform.position;
            transform.position = new Vector3(position.x, position.y, pos.z);
            _direction = direction;
        }
    }
}
