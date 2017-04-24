using System;
using System.Collections;
using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Controllers.EffectController
{
    class EffectController : BaseController<EffectController>
    {
        [SerializeField] private Pool _explosions;
        [SerializeField] private Pool _bigExplosions;
        [SerializeField] private Pool _sparkleds;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakePower;
        [SerializeField] private float _transitionDuration;
        [SerializeField] private Material _postMaterial;
        private int _counter;
        private Coroutine _shakeCoroutine;
        private Camera _camera;
        private Camera Camera { get { return _camera ?? (_camera = Camera.main); } }

        public float TransitionDuration { get { return _transitionDuration;} }
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
            _postMaterial.SetFloat("_GrayPower", 0);
        }

        public void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, _postMaterial);
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

        private Coroutine _coroutine;

        public void FadeTransition()
        {
            if (_coroutine != null) return;
            _coroutine = StartCoroutine(Fade(_transitionDuration));
        }

        private IEnumerator Fade(float duration)
        {
            yield return StartCoroutine(FadeInCoroutine(duration/2));
            yield return new WaitForSeconds(duration/2);
            yield return StartCoroutine(FadeInCoroutine(duration/2, true));
        }

        private IEnumerator FadeInCoroutine(float duration, bool inverse = false)
        {
            var inverseValue = inverse ? 1 : 0;
            var startTime = Time.time;
            var finishTime = Time.time + duration;
            while (Time.time < finishTime)
            {
                SetFadePower(Mathf.Abs(inverseValue - Mathf.InverseLerp(startTime, finishTime, Time.time)));
                yield return null;
            }
            SetFadePower(1 - inverseValue);
        }

        private void SetFadePower(float value)
        {
            _postMaterial.SetFloat("_GrayPower", value);
        }
    }
} 