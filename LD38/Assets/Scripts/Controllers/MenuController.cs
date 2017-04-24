using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    class MenuController : BaseController<MenuController>
    {
        public override void AwakeSingleton()
        {
            
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
            SceneManager.LoadScene(1);
        }
    }
}
