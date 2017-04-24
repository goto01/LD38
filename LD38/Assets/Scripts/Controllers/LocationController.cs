using System.Collections;
using Assets.Scripts.Components.Level;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class LocationController : BaseController<LocationController>
    {
        [SerializeField] private Location _currentLocation;

        public Location CurrentLocation { get { return _currentLocation;} }

        public override void AwakeSingleton()
        {
            _currentLocation.Activate();
        }
        
        public void SwitchToLocation(Location location, Door.DoorType doortype, Transform ship)
        {
            InputController.InputController.Instance.Disable();
            Debug.Log("fade");
            EffectController.EffectController.Instance.FadeTransition();
            var duration = EffectController.EffectController.Instance.TransitionDuration;
            var type = (Door.DoorType)(-(int) doortype);
            StartCoroutine(Transit(duration, location, type, ship));
        }

        private IEnumerator Transit(float duration, Location location, Door.DoorType type, Transform ship)
        {
            yield return new WaitForSeconds(duration);
            _currentLocation.gameObject.SetActive(false);
            _currentLocation = Instantiate(location);
            _currentLocation.GetComponent<Location>().HandleDoor(type, ship);
            yield return new WaitForSeconds(duration/2);
            InputController.InputController.Instance.Enable();
            yield return new WaitForSeconds(duration);
            _currentLocation.Activate();
        }
    }
}
