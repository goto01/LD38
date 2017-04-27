using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    class UIController : BaseController<UIController>
    {
        private const string AlfaTestParameter = "_AlfaTestValue";

        private readonly int Health = Animator.StringToHash("Health");
        private readonly int Room = Animator.StringToHash("Room");
        [SerializeField] private int _shipHealth;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _totalEnemies;
        [SerializeField] private Image _locaolEnemies;

        private float TotalEnemiesAlfaTestDelta
        {
            set { _totalEnemies.material.SetFloat(AlfaTestParameter, value);}
        }

        private float LocalEnemiesAlfaTestDelta
        {
            set { _locaolEnemies.material.SetFloat(AlfaTestParameter, value);}
        }

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
            TotalEnemiesAlfaTestDelta = GamePlayController.Instance.KilledEnemeisDelta;
            LocalEnemiesAlfaTestDelta = GamePlayController.Instance.KilledLocalEnemeisDelta;
        }
    }
}
