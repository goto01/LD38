using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class UIController : BaseController<UIController>
    {
        private readonly int Health = Animator.StringToHash("Health");
        private readonly int Room = Animator.StringToHash("Room");
        [SerializeField] private int _shipHealth;
        [SerializeField] private Animator _animator;
        
        public int ShipHealth
        {
            get { return _shipHealth; }
            set { _shipHealth = value; }
        }

        public override void AwakeSingleton()
        {
            
        }

        protected virtual void Update()
        {
            _animator.SetInteger(Health, _shipHealth);
            _animator.SetInteger(Room, LocationController.Instance.CurrentLocation.Id);
        }
    }
}
