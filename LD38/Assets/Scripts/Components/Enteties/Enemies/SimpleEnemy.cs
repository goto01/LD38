using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Components.Enteties.Enemies
{
    class SimpleEnemy : EnemyBase
    {
        protected override Vector2 Direction
        {
            get { return GamePlayController.Instance.GetDirectionToShip(transform.position); }
        }
    }
}
