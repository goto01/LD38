using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class CameraSizeController : BaseController<CameraSizeController>
    {
        private Camera _camera;
        [SerializeField] private int _cameraScale = 6;
        
        public int CameraScale { get { return _cameraScale; } }

        private Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }
        
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad2)) _cameraScale--;
            else if (Input.GetKeyDown(KeyCode.Keypad8)) _cameraScale++;
        }

        public void IncCameraScale()
        {
            ++_cameraScale;
        }

        public void DecCameraScale()
        {
            --_cameraScale;
        }

        protected virtual void LateUpdate()
        {
            SetCameraSize();
        }
        
        private void SetCameraSize()
        {
            Camera.orthographicSize = (float)Screen.height / _cameraScale * .03125f;
        }

        public override void AwakeSingleton()
        {
            
        }
    }
}
