using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Core.Staff.Singleton
{
    class UnitySingleton<T> where T: MonoBehaviour
    {
        private const string AwakeSingletonMethodName = "AwakeSingleton";
        private static readonly Type _type = typeof (T);

        static private T _instance;
        static private int _globalInstanceCount = 0;
        private static bool _awakeSingletonCalled = false;
        
        static public T GetSingleton()
        {
            if (!_instance)
            {
                var components = GameObject.FindObjectsOfType(_type);
                UnityEngine.Object component = null;
                foreach (var c in components)
                {
                    var singleton = (ISingletonMonoBehaviour) c;
                    if (singleton.IsSingleton)
                    {
                        component = c;
                        break;
                    }
                }
                Assert.AreNotEqual(null, component, "You don't have the instance of " + _type.Name);
                _AwakeSingleton(component as T);
            }
            return _instance;
        }

        public static void _Awake(T instance)
        {
            _globalInstanceCount++;
            Assert.IsTrue(_globalInstanceCount <= 1, "More than one instance of the " + _type.Name);
            Assert.AreNotEqual(instance, null);
            _instance = instance;
            _AwakeSingleton(instance);
        }

        private static void _AwakeSingleton(T instance)
        {
            if (_awakeSingletonCalled) return;
            _awakeSingletonCalled = true;
            instance.Invoke(AwakeSingletonMethodName, 0);
        }
    }
}