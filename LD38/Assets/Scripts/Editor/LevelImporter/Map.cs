using System;
using UnityEngine;

namespace Assets.Scripts.Editor.LevelImporter
{
    [Serializable]
    class Map
    {
        [SerializeField] private Layer[] layers;

        public Layer[] Layers { get { return layers; } }
    }
}
