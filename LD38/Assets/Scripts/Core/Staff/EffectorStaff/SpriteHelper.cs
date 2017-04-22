using Assets.Scripts.Core.PropertyAttributes;
using UnityEngine;

namespace Assets.Scripts.Core.Staff.EffectorStaff
{
    [ExecuteInEditMode]
    class SpriteHelper : BindingMonoBehaviour
    {
        private const string SpriteRect = "_SpriteRect";

        [SerializeField] [Binding(true)] private SpriteRenderer _spriteRenderer;
        private Vector4 _rect;

        private Sprite Sprite { get { return _spriteRenderer.sprite; } }
        private Material Material { get { return _spriteRenderer.material; } }

        protected virtual void Awake()
        {
            _rect = new Vector4(Sprite.rect.x / Sprite.texture.width, 
                Sprite.rect.y / Sprite.texture.height, 
                (Sprite.rect.width - 1) / Sprite.texture.width, 
                (Sprite.rect.height - 1)/ Sprite.texture.height);
        }

        protected virtual void Update()
        {
            Material.SetVector(SpriteRect, _rect);
        }
    }
}
