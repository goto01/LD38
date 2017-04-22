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

        public void OnCollisionEnter2D(Collision2D other)
        {
            gameObject.SetActive(false);
            var sparkle = EffectController.Instance.Sparkles.Pop();
            sparkle.transform.position = new Vector3(transform.position.x,
                transform.position.y,
                sparkle.transform.position.z);
            EffectController.Instance.Shake();
        }
    }
}
