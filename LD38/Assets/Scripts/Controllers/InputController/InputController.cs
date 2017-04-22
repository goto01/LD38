using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.InputController
{
    class InputController : BaseController<InputController>
    {
        [SerializeField] private List<InputItem> _inputItems; 

        public override void AwakeSingleton()
        {
            
        }

        public Vector2 GetDirection()
        {
            var direction = Vector2.zero;
            for (var index = 0; index < _inputItems.Count; index++)
                direction += _inputItems[index].GetDirection();
            return direction;
        }
    }
}
