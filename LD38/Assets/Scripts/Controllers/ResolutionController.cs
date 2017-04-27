using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class ResolutionController : BaseController<ResolutionController>
    {
        [Serializable]
        class Resolution
        {
            public Vector2 Size;
            public int Scale;
            public int CameraScale;
        }

        [SerializeField] private Resolution[] _resolutions;
        [SerializeField] private Canvas _canvas;

        public override void AwakeSingleton()
        {
        }

        protected virtual void Update()
        {
            SetBestScale();
        }

        private void SetBestScale()
        {
            var bestResolution = _resolutions[0];
            var minValue = int.MaxValue;
            var width = Screen.width;
            var height = Screen.height;
            foreach (var resolution in _resolutions)
            {
                var delta = (int)(Mathf.Abs(resolution.Size.x - width) + Math.Abs(resolution.Size.y - height));
                if (minValue > delta)
                {
                    minValue = delta;
                    var r = resolution;
                    bestResolution = r;
                }
                }
            _canvas.scaleFactor = bestResolution.Scale;
            CameraSizeController.Instance.CameraScale = bestResolution.CameraScale;
        }
    }
}
