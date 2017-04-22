using Assets.Scripts.Components.Movement;
using Assets.Scripts.Controllers;
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
        [SerializeField] private LayerMask _layerMask;

        protected override Vector2 Direction
        {
            get { return _direction; }
        }

        public void Init(Vector2 position, Vector2 direction, float speed)
        {
            _speed = speed;
            var pos = transform.position;
            transform.position = new Vector3(position.x, position.y, pos.z);
            _direction = direction;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        protected override void Update()
        {
            var @object = Physics2D.Raycast(transform.position, Offset, Offset.magnitude, _layerMask);
            if (@object.collider != null)
            {
                Collide(@object.point);
            }
            else base.Update();
        }

        public void Collide(Vector2 position)
        {
            gameObject.SetActive(false);
            var sparkle = EffectController.Instance.Sparkles.Pop();
            sparkle.transform.position = new Vector3(position.x,
                position.y,
                sparkle.transform.position.z);
            EffectController.Instance.Shake();
        }
    }
}
