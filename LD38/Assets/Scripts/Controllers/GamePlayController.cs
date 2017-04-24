using Assets.Scripts.Components.Enteties;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GamePlayController : BaseController<GamePlayController>
    {
        [SerializeField] private Ship _ship;
        [SerializeField] [Range(0, 1)] private float _enemyStop = .5f;
        [SerializeField] [Range(0, 1)] private float _enemyStopDuraton = .1f;

        public float EnemyStop { get { return _enemyStop;} }
        public float EnemyStopDuration { get { return _enemyStopDuraton;} }
        public Vector3 ShipPos { get { return _ship.transform.position; } }
        public Ship Ship{ get { return _ship; } }

        public Vector2 GetDirectionToShip(Vector2 pos)
        {
            return ((Vector2) _ship.transform.position - pos).normalized;
        }

        public override void AwakeSingleton()
        {
            
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
    }
}
