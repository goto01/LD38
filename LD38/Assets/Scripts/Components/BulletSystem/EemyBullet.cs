using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Components.Enteties;
using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EffectController;
using UnityEngine;

namespace Assets.Scripts.Components.BulletSystem
{
    class EemyBullet : Bullet
    {
        public const string Tag = "Enemy bullet";

        protected override void Collide(Vector2 position, string tag)
        {
            base.Collide(position, tag);
            if (!tag.Equals(Ship.Tag)) return;
            var ex = EffectController.Instance.BigExplosions.Pop();
            ex.transform.position = new Vector3(transform.position.x, transform.position.y,
                ex.transform.position.z);
            GamePlayController.Instance.Ship.Damage();
        }
    }
}
