using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Level
{
    [Serializable]
    class ListWrapper
    {
        public List<Transform> List;

        public ListWrapper()
        {
            List = new List<Transform>();
        }
    }

    public class Grid : BindingMonoBehaviour
    {
        [SerializeField] private List<ListWrapper> _grid;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        
        public void Init(int width, int height)
        {
            _width = width;
            _height = height;
            _grid = new List<ListWrapper>();
            for (var row = 0; row < height; row ++)
            {
                var line = new ListWrapper();
                for (var column = 0; column < width; column++) line.List.Add(null);
                _grid.Add(line);
            }
        }

        public void AddTile(GameObject tile, int row, int column)
        {
            _grid[row].List[column] = tile.transform;
            tile.transform.parent = transform;
        }

        public void Reposit(float tileWidth, float tileHeight)
        {
            var startPos = new Vector2((-_width/2 + .5f) * tileWidth, (_height/2 - .5f)*tileHeight);
            for (var row = 0; row < _height; row++)
                for (var column = 0; column < _width; column++)
                {
                    Vector3 pos = startPos + new Vector2(column*tileWidth, -row*tileHeight);
                    var tile = _grid[row].List[column];
                    if (tile == null) continue;
                    pos.z = tile.transform.position.z;
                    tile.position = pos;
                }
        }
    }
}
