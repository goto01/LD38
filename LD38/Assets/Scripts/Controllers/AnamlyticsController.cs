using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Analytics;

namespace Assets.Scripts.Controllers
{
    class AnamlyticsController : BaseController<AnamlyticsController>
    {
        private const string EnemyKilledEvent = "Enemy killed";
        private const string NewRoomEvent = "New room";

        public override void AwakeSingleton()
        {
        }

        public void SendEnemyKilled()
        {
            Analytics.CustomEvent(EnemyKilledEvent);
        }

        public void SendNewRoom()
        {
            Analytics.CustomEvent(NewRoomEvent);
        }
    }
}
