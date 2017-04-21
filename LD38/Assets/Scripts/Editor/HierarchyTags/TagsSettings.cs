using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.HierarchyTags
{
    class TagsSettings : ScriptableObject
    {
        [Serializable]
        internal class TitleDescription
        {
            public string Name;
            public Color Color;
        }

        [SerializeField] private List<TitleDescription> _tagDescriptions;
        [SerializeField] private List<TitleDescription> _layerDescriptions;
        
        public TitleDescription[] Tags { get { return _tagDescriptions.ToArray(); } }
        public TitleDescription[] Layers { get { return _layerDescriptions.ToArray(); } }

        public Color GetColorForTag(string tag)
        {
            var item = GetTagDescription(tag) ?? AddTagDescription(tag);
            return item.Color;
        }

        public Color GetColorForLayer(string layer)
        {
            var item = GetLayerDescription(layer) ?? AddLayerDescription(layer);
            return item.Color;
        }

        private TitleDescription GetTagDescription(string name)
        {
            return _tagDescriptions.FirstOrDefault(x => x.Name.Equals(name));
        }

        private TitleDescription GetLayerDescription(string name)
        {
            return _layerDescriptions.FirstOrDefault(x => x.Name.Equals(name));
        }

        private TitleDescription AddTagDescription(string tag)
        {
            var item = new TitleDescription() {Name = tag, Color = Color.black};
            _tagDescriptions.Add(item);
            EditorUtility.SetDirty(this);
            return item;
        }

        private TitleDescription AddLayerDescription(string layer)
        {
            var item = new TitleDescription() {Name = layer, Color = Color.black};
            _layerDescriptions.Add(item);
            EditorUtility.SetDirty(this);
            return item;
        }
    }
}
