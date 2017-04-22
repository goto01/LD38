using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.InputController
{
    class InputController : BaseController<InputController>
    {
        [SerializeField] private InputItemButton _shotButton;
        [SerializeField] private List<InputItem> _inputItems; 

        public bool Shot { get { return _shotButton.State; } }

        public override void AwakeSingleton()
        {
        }

        public Vector2 GetDirectionToPointer(Vector2 position)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (mousePosition - position).normalized;
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
