using System;
using Assets.Scripts.Core.Staff.Helpers;
using UnityEngine;

namespace Assets.Scripts.Core.Staff.Pool
{
    class PoolableItem : BindingMonoBehaviour
    {
        public event EventHandler Disable;

        public void Activate()
        {
            
        }

        protected virtual void OnDisable()
        {
            Disable.Raise(this);
        }
    }
}
