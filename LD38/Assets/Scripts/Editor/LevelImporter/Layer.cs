using System;
using UnityEngine;

namespace Assets.Scripts.Editor.LevelImporter
{
    [Serializable]
    class Layer
    {
        [SerializeField] private int[] data;
        [SerializeField] private int height;
        [SerializeField] private int width;

        public int[] Data { get { return data; } }
        public int Height { get { return height; } }
        public int Width { get { return width; } }
    }
}
