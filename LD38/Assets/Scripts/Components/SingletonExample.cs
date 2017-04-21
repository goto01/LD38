using UnityEngine;

namespace Assets.Scripts.Components
{
    class SingletonExample : Core.Staff.Singleton.SingletonMonoBehaviour<SingletonExample>
    {
        public override void AwakeSingleton()
        {
            Debug.Log("Singleton example");
        }
    }
}
