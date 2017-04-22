using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class GamePlayController : BaseController<GamePlayController>
    {
        [SerializeField] private Transform _ship;

        public Vector2 GetDirectionToShip(Vector2 pos)
        {
            return ((Vector2) _ship.transform.position - pos).normalized;
        }

        public override void AwakeSingleton()
        {
            
        }
    }
}
