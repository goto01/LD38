using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    class MenuController : BaseController<MenuController>
    {
        [SerializeField] private int _scene;
        
        public override void AwakeSingleton()
        {
            AudioController.Play("Menu");
        }

        protected virtual void Update()
        {
            if (Input.GetMouseButtonDown(0))
                StartCoroutine(LateSwitch());
        }

        private IEnumerator LateSwitch()
        {
            EffectController.EffectController.Instance.FadeIn();
            yield return new WaitForSeconds(EffectController.EffectController.Instance.TransitionDuration/2);
            SceneManager.LoadScene(_scene);
        }
    }
}
