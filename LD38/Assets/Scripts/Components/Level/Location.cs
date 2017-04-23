using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Level
{
    public class Location : BindingMonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Grid _enemiesGrid;
        [SerializeField] private int _tileWidth;
        [SerializeField] private int _tileHeight;
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        public Grid Grid { get { return _grid; } }
        public Grid EnemiesGrid { get { return _enemiesGrid; } }

        private float TileWidth { get { return _tileWidth*.03125f; } }
        private float TileHeight { get { return _tileHeight*.03125f; } }

        public void Init(int width, int height)
        {
            _grid.Init(width, height);
            _enemiesGrid.Init(width, height);
        }

        public void Reposit()
        {
            _grid.Reposit(_tileWidth * 0.03125f, _tileHeight * 0.03125f);
            _enemiesGrid.Reposit(_tileWidth * 0.03125f, _tileHeight * 0.03125f);
        }
    }
}
