using System;
using UnityEngine;

namespace Assets.Scripts.Editor.LevelImporter
{
    [Serializable]
    class Map
    {
        [SerializeField] private Layer[] layers;
        [SerializeField] private TileSet[] tilesets;

        public Layer[] Layers { get { return layers; } }
    }
}
