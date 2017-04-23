using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.LevelImporter.TileDescription
{
    class TilesRepository : ScriptableObject
    {
        [Serializable]
        class TileDescription
        {
            public Transform Tile;
            public int Id;
        }

        [SerializeField] private List<TileDescription> _tiles;

        [MenuItem("Assets/Create/Tiles repository")]
        public static void CreateSelf()
        {
            var asset = CreateInstance<TilesRepository>();
            ProjectWindowUtil.CreateAsset(asset, string.Format("{0}.asset", "Tiles repository"));
        }

        public Transform GetTile(int id)
        {
            for (var index = 0; index < _tiles.Count; index++)
                if (_tiles[index].Id.Equals(id)) return _tiles[index].Tile;
            return null;
        }
    }
}
