using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EffectController;
using Assets.Scripts.Core.PropertyAttributes;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Level
{
    class Door : BindingMonoBehaviour
    {
        private readonly int OpenedParameter = Animator.StringToHash("Opened");

        [SerializeField] [Binding(true)] private Animator _animator;
        [SerializeField] private bool _opened = false;
        [SerializeField] private Location _nextLocation;

        private bool Opened
        {
            get { return _opened;}
            set
            {
                _opened = value;
                _animator.SetBool(OpenedParameter, value);
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (_opened) LocationController.Instance.SwitchToLocation(_nextLocation);
        }

        public void Close()
        {
            Opened = false;
        }

        public void Open()
        {
            Opened = true;
        }
    }
}
