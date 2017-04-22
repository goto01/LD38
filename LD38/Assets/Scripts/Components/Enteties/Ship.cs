using Assets.Scripts.Components.BulletSystem;
using Assets.Scripts.Controllers.InputController;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Enteties
{
    class Ship : BindingMonoBehaviour
    {
        [SerializeField] private BulletSpawner _bulletSpawner;

        private bool TimeToMakeShot { get { return InputController.Instance.Shot; } }

        protected virtual void Update()
        {
            if (TimeToMakeShot) MakeShot();
        }

        private void MakeShot()
        {
            _bulletSpawner.Spawn(transform.position, InputController.Instance.GetDirectionToPointer(transform.position));
        }
    }
}
