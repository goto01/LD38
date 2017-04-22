using System;
using UnityEngine;

namespace Assets.Scripts.Controllers.InputController
{
    [Serializable]
    class InputItem
    {
        [SerializeField] private string[] _inputs;
        [SerializeField] private Vector2 _direction;

        public Vector2 GetDirection()
        {
            for (var index = 0; index<_inputs.Length; index++)
                if (Input.GetButton(_inputs[index]))
                    return _direction;
            return Vector2.zero;
        }
    }
}
