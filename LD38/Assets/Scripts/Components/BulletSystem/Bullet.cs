using Assets.Scripts.Components.Enteties;
using Assets.Scripts.Components.Movement;
using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EffectController;
using Assets.Scripts.Core.PropertyAttributes;
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
        [SerializeField] [Binding(true)] protected CircleCollider2D _circleCollider2D;

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
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        protected override void Update()
        {
            var offsetm = Mathf.Max(Offset.magnitude, _circleCollider2D.radius * 0.04f);
            var @object = Physics2D.CircleCast(transform.position, _circleCollider2D.radius * 0.03125f, Offset, offsetm, _layerMask);
            if (@object.collider != null)
            {
                var entity = @object.collider.GetComponent<IEntity>();
                if (entity != null) entity.Damage();
                Collide(@object.point, @object.collider.tag);
            }
            else base.Update();
        }

        protected virtual void Collide(Vector2 position, string tag)
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
