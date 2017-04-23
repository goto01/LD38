using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class UIController : BaseController<UIController>
    {
        private readonly int Health = Animator.StringToHash("Health");
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
        }
    }
}
