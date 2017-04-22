using Assets.Scripts.Controllers.InputController;
using UnityEngine;

namespace Assets.Scripts.Components.Movement
{
    class CharacterMovement : BaseMovement
    {
        protected override Vector2 Direction
        {
            get { return InputController.Instance.GetDirection(); }
        }

        protected override void Translate()
        {
            _rigidbody2D.AddForce(Offset);
        }
    }
}
