using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class EffectController : BaseController<EffectController>
    {
        [SerializeField] private Pool _explosions;

        public Pool Explosions { get { return _explosions;} }

        public override void AwakeSingleton()
        {
        }
    }
}
