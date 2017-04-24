using System;
using System.Collections.Generic;
using Assets.Scripts.Components.Enteties.Enemies;
using Assets.Scripts.Core.Staff;
using UnityEngine;

namespace Assets.Scripts.Components.Level
{
    [Serializable]
    public class TransformExt
    {
        public Transform Transform;
        public Vector3 Origin;
    }

    [Serializable]
    public class ListWrapper
    {
        public List<TransformExt> List;

        public ListWrapper()
        {
            List = new List<TransformExt>();
        }
    }

    public class Grid : BindingMonoBehaviour
    {
        [SerializeField] private List<ListWrapper> _grid;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private List<Transform> _cash; 

        public List<Transform> Cash { get { return _cash; } } 
        public List<ListWrapper> GridData { get { return _grid; } } 
        
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

        public void AppendTile(GameObject tile)
        {
            _cash.Add(tile.transform);
        }

        public virtual void AddTile(GameObject tile, int row, int column)
        {
            _grid[row].List[column] = new TransformExt();
            _grid[row].List[column].Transform = tile.transform;
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
                    if (tile == null || tile.Transform == null) continue;
                    pos.z = tile.Transform.position.z;
                    tile.Origin = pos;
                    tile.Transform.position = pos;
                }
        }

        public void ResetSelf()
        {
            for (var row = 0; row < _height; row++)
                for (var column = 0; column < _width; column++)
                {
                    var tile = _grid[row].List[column];
                    if (tile.Transform == null) continue;
                    tile.Transform.position = new Vector3(tile.Origin.x, tile.Origin.y, tile.Transform.transform.position.z);
                }
    }
    }
}
