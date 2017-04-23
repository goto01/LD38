using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Level
{
    public class Location : BindingMonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private int _tileWidth;
        [SerializeField] private int _tileHeight;

        public Grid Grid { get { return _grid; } }

        public void Init(int width, int height)
        {
            _grid.Init(width, height);
        }

        public void Reposit()
        {
            _grid.Reposit(_tileWidth * 0.03125f, _tileHeight * 0.03125f);
        }
    }
}
