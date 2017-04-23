using System.Collections;
using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class EffectController : BaseController<EffectController>
    {
        [SerializeField] private Pool _explosions;
        [SerializeField] private Pool _bigExplosions;
        [SerializeField] private Pool _sparkleds;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakePower;
        private int _counter;
        private Coroutine _shakeCoroutine;
        private Camera _camera;
        private Camera Camera { get { return _camera ?? (_camera = Camera.main); } }

        public Pool Explosions { get { return _explosions;} }
        public Pool BigExplosions { get { return _bigExplosions;} }

        public Pool Sparkles
        {
            get
            {
                if (_counter ++ == 3)
                {
                    _counter = 0;
                    return _explosions;
                }
                return _sparkleds;
            }
        }

        public override void AwakeSingleton()
        {
        }

        private IEnumerator Shake(float duration, float power)
        {
            var truePosition = Camera.transform.position;
            var finishTime = Time.time + duration;
            while (Time.time < finishTime)
            {
                Camera.transform.position = truePosition;
                var xOffset = Random.Range(0, power * 2) - power;
                var yOffset = Random.Range(0, power * 2) - power;
                Camera.transform.position += new Vector3(xOffset, yOffset, 0);
                yield return null;
            }
            Camera.transform.position = truePosition;
            _shakeCoroutine = null;
        }

        public void Shake()
        {
            StopShake();
            _shakeCoroutine = StartCoroutine(Shake(_shakeDuration, _shakePower));
        }

        public void BigShake()
        {
            StopShake();
            _shakeCoroutine = StartCoroutine(Shake(_shakeDuration, _shakePower * 1.5f));
        }

        public void StopShake()
        {
            if (_shakeCoroutine != null)
            {
                StopCoroutine(_shakeCoroutine);
                _shakeCoroutine = null;
            }
        }

    }
} 