using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Controllers.EffectController
{
    class CameraComponent : BindingMonoBehaviour
    {
        public const string Tag = "MainCamera";

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Debug.Log(gameObject.tag);
            Debug.Log(name);
            EffectController.Instance.OnRenderImage(src, dest);
        }
    }
}
