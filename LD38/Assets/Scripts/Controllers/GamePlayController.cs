using Assets.Scripts.Components.Enteties;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GamePlayController : BaseController<GamePlayController>
    {
        [SerializeField] private Ship _ship;
        [SerializeField] [Range(0, 1)] private float _enemyStop = .5f;
        [SerializeField] [Range(0, 1)] private float _enemyStopDuraton = .1f;
        [SerializeField] private int _totalKilledEnemiesNumber = 90;
        [SerializeField] private int _currentKilledEnemiesNumber = 0;
        [SerializeField] private float _killedEnemiesDelta = 0;

        [SerializeField] private int _totalKilledLocalEnemiesNumber = 6;
        [SerializeField] private int _currentKilledLocalEnemiesNumber = 0;
        [SerializeField] private float _killedLocalEnemiesDelta = 0;

        public float EnemyStop { get { return _enemyStop;} }
        public float EnemyStopDuration { get { return _enemyStopDuraton;} }
        public Vector3 ShipPos { get { return _ship.transform.position; } }
        public Ship Ship{ get { return _ship; } }

        public int TotalLocalKilledEnemiesNumber
        {
            set
            {
                _totalKilledLocalEnemiesNumber = value; 
                ReseltLocalKilledEnemies();
            }
        }

        public float KilledEnemeisDelta
        {
            get { return _killedEnemiesDelta; }
        }

        public float KilledLocalEnemeisDelta
        {
            get { return _killedLocalEnemiesDelta; }
        }

        public Vector2 GetDirectionToShip(Vector2 pos)
        {
            return ((Vector2) _ship.transform.position - pos).normalized;
        }

        public override void AwakeSingleton()
        {
            
        }

        public void IncKilledEnemiesNumber()
        {
            _currentKilledEnemiesNumber++;
            _currentKilledLocalEnemiesNumber++;
            UpdateDeltas();
        }

        public void ReseltLocalKilledEnemies()
        {
            _currentKilledLocalEnemiesNumber = 0;
            UpdateDeltas();
        }

        public void ResetKilledEnemies()
        {
            _currentKilledEnemiesNumber = 0;
            ReseltLocalKilledEnemies();
        }

        protected virtual void Update()
        {
            if (_ship.CurrentHealth == 0)
            {
                PoolsHandlerController.Instance.ResetSelf();
                InputController.InputController.Instance.Disable();
                var ex = EffectController.EffectController.Instance.BigExplosions.Pop();
                ex.transform.position = new Vector3(_ship.transform.position.x, 
                    _ship.transform.position.y,
                    ex.transform.position.z);
                EffectController.EffectController.Instance.FadeTransition();
                LocationController.Instance.ResetSelf();
                _ship.ResetSelf();
            }
        }

        private void UpdateDeltas()
        {
            _killedEnemiesDelta = Mathf.InverseLerp(0, _totalKilledEnemiesNumber, _currentKilledEnemiesNumber);
            _killedLocalEnemiesDelta = Mathf.InverseLerp(0, _totalKilledLocalEnemiesNumber, _currentKilledLocalEnemiesNumber);
        }
    }
}
