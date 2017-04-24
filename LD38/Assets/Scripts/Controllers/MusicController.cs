using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class MusicController : BaseController<MusicController>
    {
        protected virtual void Start()
        {
            StartCoroutine(LateMusic());
        }

        private IEnumerator LateMusic()
        {
            yield return new WaitForSeconds(1);
            Debug.Log("ALERT");
            AudioController.PlayMusicPlaylist("Music");
        }

        public override void AwakeSingleton()
        {
            
        }
    }
}
