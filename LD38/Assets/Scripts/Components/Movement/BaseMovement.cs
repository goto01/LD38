using Assets.Scripts.Core.PropertyAttributes;
using Assets.Scripts.Core.Staff;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Components.Movement
{
    public abstract class BaseMovement : BindingMonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] [Binding(true)] protected Rigidbody2D _rigidbody2D;

        protected abstract Vector2 Direction { get; }

        private Vector2 Offset { get { return Direction * _speed  * Time.deltaTime; } }

        protected virtual void Update()
        {
            Translate();
        }

        private void Translate()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + Offset);
        }
    }
}
