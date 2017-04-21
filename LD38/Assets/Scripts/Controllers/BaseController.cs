namespace Assets.Scripts.Controllers
{
    public abstract class BaseController<T> : Core.Staff.Singleton.SingletonMonoBehaviour<T> where T : Core.Staff.Singleton.SingletonMonoBehaviour<T>
    {
        public const string Tag = "GameController";

        protected virtual void Reset()
        {
            gameObject.tag = Tag;
        }
    }
}
