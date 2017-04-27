using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Components.Level;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class LocationController : BaseController<LocationController>
    {
        [SerializeField] private Location _origin;
        [SerializeField] private Location _currentLocation;
        [SerializeField] private List<int> _oldLocations; 

        public Location CurrentLocation { get { return _currentLocation;} }

        public override void AwakeSingleton()
        {
            _origin = _currentLocation;
            _currentLocation.Activate();
        }
        
        public void SwitchToLocation(Location location, Door.DoorType doortype, Transform ship)
        {
            AnamlyticsController.Instance.SendNewRoom();
            InputController.InputController.Instance.Disable();
            EffectController.EffectController.Instance.FadeTransition();
            var duration = EffectController.EffectController.Instance.TransitionDuration;
            var type = (Door.DoorType)(-(int) doortype);
            StartCoroutine(Transit(duration, location, type, ship));
            GamePlayController.Instance.TotalLocalKilledEnemiesNumber = location.EnemiesNumber;
            GamePlayController.Instance.ReseltLocalKilledEnemies();
        }

        public void ResetSelf()
        {
            _oldLocations.Clear();
            _oldLocations.Add(_origin.Id);
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
            if (_oldLocations.Contains(_currentLocation.Id)) _currentLocation.DisableEnemies();
            yield return new WaitForSeconds(duration/2);
            InputController.InputController.Instance.Enable();
            yield return new WaitForSeconds(duration);
            if (!_oldLocations.Contains(_currentLocation.Id))
            {
                _currentLocation.Activate();
                _oldLocations.Add(_currentLocation.Id);
            }
            yield return new WaitForSeconds(EffectController.EffectController.Instance.TransitionDuration);
            InputController.InputController.Instance.Enable();
            _currentLocation.Block = false;
        }
    }
}
