using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components
{
    class GhostComponent : BindingMonoBehaviour
    {
        [SerializeField] private Transform _parent;

        protected virtual void Update()
        {
            transform.position = _parent.position;
        }
    }
}
