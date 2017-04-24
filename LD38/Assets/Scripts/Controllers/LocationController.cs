using System.Collections;
using Assets.Scripts.Components.Level;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class LocationController : BaseController<LocationController>
    {
        [SerializeField] private Location _origin;
        [SerializeField] private Location _currentLocation;

        public Location CurrentLocation { get { return _currentLocation;} }

        public override void AwakeSingleton()
        {
            _origin = _currentLocation;
            _currentLocation.Activate();
        }
        
        public void SwitchToLocation(Location location, Door.DoorType doortype, Transform ship)
        {
            InputController.InputController.Instance.Disable();
            EffectController.EffectController.Instance.FadeTransition();
            var duration = EffectController.EffectController.Instance.TransitionDuration;
            var type = (Door.DoorType)(-(int) doortype);
            StartCoroutine(Transit(duration, location, type, ship));
        }

        public void ResetSelf()
        {
            StartCoroutine(ResetCoroutine());
        }

        private IEnumerator ResetCoroutine()
        {
            yield return new WaitForSeconds(EffectController.EffectController.Instance.TransitionDuration/2);
            _currentLocation.gameObject.SetActive(false);
            _currentLocation = Instantiate(_origin);
            _currentLocation.gameObject.SetActive(true);
            _currentLocation.Activate();
            InputController.InputController.Instance.Enable();
        }

        private IEnumerator Transit(float duration, Location location, Door.DoorType type, Transform ship)
        {
            GamePlayController.Instance.Ship.IncHealth();
            yield return new WaitForSeconds(duration);
            _currentLocation.gameObject.SetActive(false);
            _currentLocation = Instantiate(location);
            _currentLocation.GetComponent<Location>().HandleDoor(type, ship);
            yield return new WaitForSeconds(duration/2);
            InputController.InputController.Instance.Enable();
            yield return new WaitForSeconds(duration);
            _currentLocation.Activate();
            yield return new WaitForSeconds(EffectController.EffectController.Instance.TransitionDuration);
            InputController.InputController.Instance.Enable();
        }
    }
}
