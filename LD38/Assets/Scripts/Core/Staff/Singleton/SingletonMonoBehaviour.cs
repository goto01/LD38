using UnityEngine;

namespace Assets.Scripts.Core.Staff.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour, ISingletonMonoBehaviour
        where T: SingletonMonoBehaviour<T>
    {
        public bool IsSingleton { get {return true;} }

        public static T Instance { get { return UnitySingleton<T>.GetSingleton(); } }

        protected virtual void Awake()
        {
            if (IsSingleton) UnitySingleton<T>._Awake(this as T);
        }

        public abstract void AwakeSingleton();

        protected virtual void OnDisable()
        {
            UnitySingleton<T>.Decrease();
        }
    }
}
