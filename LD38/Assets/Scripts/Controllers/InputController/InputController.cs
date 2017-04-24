using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.InputController
{
    class InputController : BaseController<InputController>
    {
        [SerializeField] private InputItemButton _shotButton;
        [SerializeField] private List<InputItem> _inputItems;
        [SerializeField] private bool _disable = false;

        public bool Shot { get { return _disable ? false : _shotButton.State; } }
        public bool Disabled { get { return _disable; } }

        public override void AwakeSingleton()
        {
        }

        public void Disable()
        {
            _disable = true;
        }

        public void Enable()
        {
            _disable = false;
        }

        public Vector2 GetDirectionToPointer(Vector2 position)
        {
            if (_disable) return Vector2.zero;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (mousePosition - position).normalized;
        }

        public Vector2 GetDirection()
        {
            if (_disable) return Vector2.zero;
            var direction = Vector2.zero;
            for (var index = 0; index < _inputItems.Count; index++)
                direction += _inputItems[index].GetDirection();
            return direction;
        }
    }
}
