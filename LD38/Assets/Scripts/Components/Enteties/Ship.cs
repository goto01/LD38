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
            var pos = (Vector3)InputController.Instance.GetDirectionToPointer(transform.position);
            var angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        }

        private void MakeShot()
        {
            _bulletSpawner.Spawn(transform.position, InputController.Instance.GetDirectionToPointer(transform.position));
        }
    }
}
