using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Components.Enteties.Enemies
{
    class SpawnEnemy : SimpleEnemy
    {
        [SerializeField] private int _spawnNumber = 3;

        protected override void Die()
        {
            for (var index = 0; index < _spawnNumber; index++)
            {
                var enemy = Spawn();
                enemy.Activate();
                var pos = new Vector3(transform.position.x + Random.Range(-.1f, .1f),
                    transform.position.y + Random.Range(-.1f, .1f),
                    enemy.transform.position.z);
                enemy.transform.position = pos;
                var ex = EffectController.Instance.Explosions.Pop();
                ex.transform.position = new Vector3(pos.x, pos.y, ex.transform.position.z);
            }
            base.Die();
        }

        protected virtual EnemyBase Spawn()
        {
            return PoolsHandlerController.Instance.MiddleBlobPool.Pop<EnemyBase>();
        }
    }
}
