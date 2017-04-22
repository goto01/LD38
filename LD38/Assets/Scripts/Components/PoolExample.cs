using System.Collections;
using Assets.Scripts.Core.Staff.Pool;
using UnityEngine;

namespace Assets.Scripts.Components
{
    class PoolExample : MonoBehaviour
    {
        [SerializeField] private Pool _pool;

        protected virtual void Start()
        {
            StartCoroutine(PoolCoroutine());
        }

        private IEnumerator PoolCoroutine()
        {
            while (true)
            {
                var item = _pool.Pop();
                yield return new WaitForSeconds(1);
                item.gameObject.SetActive(false);
            }
        }
    }
}
