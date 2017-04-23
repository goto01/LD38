using Assets.Scripts.Controllers;

namespace Assets.Scripts.Components.Enteties.Enemies
{
    class MiddleSpawnEnemy : SpawnEnemy
    {
        protected override EnemyBase Spawn()
        {
            return PoolsHandlerController.Instance.SmallBlobPool.Pop<EnemyBase>();
        }
    }
}
