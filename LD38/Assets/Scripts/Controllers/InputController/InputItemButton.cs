using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.InputController
{
    [Serializable]
    class InputItemButton
    {
        enum ButtonType
        {
            Pressed,
            Down
        }

        [SerializeField] private List<string> _inputs;
        [SerializeField] private ButtonType _buttonType;

        public bool State
        {
            get
            {
                if (_buttonType == ButtonType.Down)
                {
                    for (var index = 0; index < _inputs.Count; index++)
                        if (Input.GetButtonDown(_inputs[index])) return true;
                }
                else
                {
                    for (var index = 0; index < _inputs.Count; index++)
                        if (Input.GetButton(_inputs[index])) return true;
                }
                return false;
            }
        }
    }
}
