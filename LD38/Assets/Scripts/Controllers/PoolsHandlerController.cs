using System;
using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class PoolsHandlerController : BaseController<PoolsHandlerController>
    {
        [SerializeField] private Pool _shipBullets;
        [SerializeField] private Pool _middleBlobPool;
        [SerializeField] private Pool _smallBlobPool;
        [SerializeField] private Pool _enemyBullets;

        public Pool ShipBullets { get { return _shipBullets; } }
        public Pool MiddleBlobPool { get {return _middleBlobPool;} }
        public Pool SmallBlobPool { get {return _smallBlobPool;} }
        public Pool EnemyBullets { get {return _enemyBullets;} }

        public override void AwakeSingleton()
        {
        }

        public void ResetSelf()
        {
            _middleBlobPool.ResetSelf();
            _smallBlobPool.ResetSelf();
        }
    }
}
