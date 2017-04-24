using System.Collections;
using Assets.Scripts.Components.Level;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class LocationController : BaseController<LocationController>
    {
        [SerializeField] private Location _currentLocation;

        public override void AwakeSingleton()
        {
        }
        
        public void SwitchToLocation(Location location)
        {
            InputController.InputController.Instance.Disable();
            EffectController.EffectController.Instance.FadeTransition();
            var duration = EffectController.EffectController.Instance.TransitionDuration;
            StartCoroutine(Transit(duration, location));
        }

        private IEnumerator Transit(float duration, Location location)
        {
            yield return new WaitForSeconds(duration);
            _currentLocation.gameObject.SetActive(false);
            _currentLocation = Instantiate(location);
            InputController.InputController.Instance.Enable();
        }
    }
}
