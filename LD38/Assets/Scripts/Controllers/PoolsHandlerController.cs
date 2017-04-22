using System;
using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class PoolsHandlerController : BaseController<PoolsHandlerController>
    {
        [SerializeField] private Pool _shipBullets;

        public Pool ShipBullets { get { return _shipBullets; } }

        public override void AwakeSingleton()
        {
        }
    }
}
