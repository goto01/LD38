namespace Assets.Scripts.Core.Staff.Singleton
{
    public interface ISingletonMonoBehaviour
    {
        bool IsSingleton { get; }

        void AwakeSingleton();
    }
}
