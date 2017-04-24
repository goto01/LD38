using System.Collections;
using System.Runtime.Remoting.Channels;
using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.EffectController;
using Assets.Scripts.Core.PropertyAttributes;
using Assets.Scripts.Core.Staff;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.Components.Level
{
    public class Door : BindingMonoBehaviour
    {
        public enum DoorType
        {
            Left = -1,
            Right = 1,
            Up = -2,
            Down = 2
        }

        private readonly int OpenedParameter = Animator.StringToHash("Opened");

        [SerializeField] [Binding(true)] private Animator _animator;
        [SerializeField] private bool _opened = false;
        [SerializeField] private Location _nextLocation;
        [SerializeField] private DoorType _doorType;
        [SerializeField] private Vector2 _enterOffset;

        public DoorType Type { get { return _doorType; } }

        private bool Opened
        {
            get { return _opened;}
            set
            {
                _opened = value;
                _animator.SetBool(OpenedParameter, value);
            }
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            var pos = transform.position;
            pos += (Vector3)_enterOffset;
            Handles.SphereCap(0, pos, Quaternion.Euler(1,1,1), .1f);
        }
#endif

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (_opened)
            {
                var ship = other.transform;
                LocationController.Instance.SwitchToLocation(_nextLocation, _doorType, ship);
                var duration = EffectController.Instance.TransitionDuration/2;
                StartCoroutine(MoveTo(duration, ship, ship.position,
                    transform.position));
            }
        }

        private IEnumerator MoveTo(float duration, Transform ship, Vector2 from, Vector2 to)
        {
            var time = Time.time;
            while (time + duration > Time.time)
            {
                var delta = Mathf.InverseLerp(time, time + duration, Time.time);
                var x = Mathf.Lerp(from.x, to.x, delta);
                var y = Mathf.Lerp(from.y, to.y, delta);
                ship.transform.position = new Vector3(x,y, ship.position.z);
                yield return null;
            }
        }

        public void HandleEnterShip(Transform ship)
        {
            ship.transform.position = new Vector3(transform.position.x, transform.position.y,
                transform.position.z);
            StartCoroutine(MoveTo(EffectController.Instance.TransitionDuration/2, ship,
                ship.position, transform.position + (Vector3)_enterOffset));
        }

        public void Close()
        {
            Opened = false;
        }

        public void Open()
        {
            Opened = true;
        }
    }
}
