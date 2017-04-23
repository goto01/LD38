using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Controllers.EffectController
{
    class CameraComponent : BindingMonoBehaviour
    {
        public const string Tag = "MainCamera";

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            EffectController.Instance.OnRenderImage(src, dest);
        }
    }
}
