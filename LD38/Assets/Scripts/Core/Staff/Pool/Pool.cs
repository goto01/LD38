using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Staff.Pool
{
    class Pool : BindingMonoBehaviour
    {
        [SerializeField] private PoolableItem _source;
        [SerializeField] private int _number = 20;
        [SerializeField] private List<PoolableItem> _items;

        protected virtual void Awake()
        {
            for (var index = 0; index < _number; index++)
            {
                var item = Instantiate(_source);
                item.gameObject.SetActive(false);
                item.transform.parent = transform;
                item.name = string.Format("{0} {1}", _source.name, index);
                _items.Add(item);
            }
        }

        public T Pop<T>()
        {
            return Pop().gameObject.GetComponent<T>();
        }

        public PoolableItem Pop()
        {
            var item = _items[0];
            _items.RemoveAt(0);
            item.gameObject.SetActive(true);
            item.Disable += ItemOnDisable;
            return item;
        }

        private void ItemOnDisable(object sender, EventArgs eventArgs)
        {
            var item = sender as PoolableItem;
            _items.Add(item);
        }
    }
}
