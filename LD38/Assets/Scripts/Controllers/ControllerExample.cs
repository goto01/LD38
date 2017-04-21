using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class ControllerExample : BaseController<ControllerExample>
    {
        public override void AwakeSingleton()
        {
            Debug.Log("Controller example");
        }
    }
}
