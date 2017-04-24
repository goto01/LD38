using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core.Staff;

namespace Assets.Scripts.Components
{
    class MusicComponent : BindingMonoBehaviour
    {
        public void PlayAudio(string audio)
        {
            AudioController.Play(audio);
        }
    }
}
