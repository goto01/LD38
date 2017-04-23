using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GamePlayController : BaseController<GamePlayController>
    {
        [SerializeField] private Transform _ship;
        [SerializeField] [Range(0, 1)] private float _enemyStop = .5f;
        [SerializeField] [Range(0, 1)] private float _enemyStopDuraton = .1f;

        public float EnemyStop { get { return _enemyStop;} }
        public float EnemyStopDuration { get { return _enemyStopDuraton;} }

        public Vector2 GetDirectionToShip(Vector2 pos)
        {
            return ((Vector2) _ship.transform.position - pos).normalized;
        }

        public override void AwakeSingleton()
        {
            
        }
    }
}
