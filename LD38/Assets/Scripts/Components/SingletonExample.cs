using Assets.Scripts.Core.Staff.Singleton;
using UnityEngine;

namespace Assets.Scripts.Components
{
    class SingletonExample : SingletonMonoBehaviour<SingletonExample>
    {
        public override void AwakeSingleton()
        {
            Debug.Log("Singleton example");
        }
    }
}
