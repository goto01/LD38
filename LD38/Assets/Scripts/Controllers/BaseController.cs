using Assets.Scripts.Core.Staff.Singleton;

namespace Assets.Scripts.Controllers
{
    public abstract class BaseController<T> : SingletonMonoBehaviour<T> where T : SingletonMonoBehaviour<T>
    {
        public const string Tag = "GameController";

        protected virtual void Reset()
        {
            gameObject.tag = Tag;
        }
    }
}
