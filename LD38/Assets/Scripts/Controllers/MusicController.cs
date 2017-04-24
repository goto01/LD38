using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Controllers
{
    class MusicController : BaseController<MusicController>
    {
        public override void AwakeSingleton()
        {
            AudioController.PlayMusicPlaylist("Music");
        }
    }
}
