using System.Collections;
using Assets.Scripts.Components.BulletSystem;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Components.Enteties.Enemies
{
    class ShootableEnemy : SimpleEnemy
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _bulletSpeed;

        private Coroutine _coroutine;
        
        public override void Activate()
        {
            base.Activate();
            _coroutine = StartCoroutine(Shot());
        }

        protected virtual void OnDisable()
        {
            StopCoroutine(_coroutine);
        }

        public void SpawnBullet()
        {
            var bullet = PoolsHandlerController.Instance.EnemyBullets.Pop<Bullet>();
            bullet.Init(transform.position, GamePlayController.Instance.GetDirectionToShip((Vector2)transform.position), _bulletSpeed);
        }

        private IEnumerator Shot()
        {
            while (true)
            {
                _animator.SetTrigger("Shot");
                yield return new WaitForSeconds(_shootDelay);
            }
        }

        protected override void Update()
        {
            base.Update();
            var pos = GamePlayController.Instance.GetDirectionToShip(transform.position);
            var angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        protected override void Translate()
        {
            _delta = 1;
            if (Vector2.Distance(transform.position, (Vector2) GamePlayController.Instance.ShipPos) < _maxDistance)
            {
                _delta = -1;
            }
            base.Translate();
        }
    }
}
