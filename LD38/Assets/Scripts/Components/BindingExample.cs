using Assets.Scripts.Core.PropertyAttributes;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components
{
    class BindingExample : BindingMonoBehaviour
    {
        [SerializeField] [Binding(true)] private SpriteRenderer _spriteRenderer;

        protected virtual void Start()
        {
            _spriteRenderer.color = Color.blue;
        }
    }
}
