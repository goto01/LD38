using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.HierarchyTags
{
    [InitializeOnLoad]
    class HierarchyTags
    {
        private const string TagsSettingsFile = "TagsSettings.asset";
        private const string EditorDefaultResources = "Editor Default Resources";
        private static TagsSettings _tagsSettings;

        public static TagsSettings TagsSettings { get { return _tagsSettings; } }

        static HierarchyTags()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGui;
            _tagsSettings = LoadTagsSettings();
        }

        private static TagsSettings LoadTagsSettings()
        {
            var settings = EditorGUIUtility.Load(TagsSettingsFile) as TagsSettings;
            if (settings != null) return settings;
            var asset = ScriptableObject.CreateInstance<TagsSettings>();
            AssetDatabase.CreateAsset(asset, "Assets/" + EditorDefaultResources + "/" + TagsSettings);
            return asset;
        }

        private static void HierarchyWindowItemOnGui(int instanceId, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (gameObject == null) return;
            var tagRect = selectionRect;
            tagRect.x += tagRect.width/3;
            DrawTagTitle(tagRect, gameObject.tag);
            var layerRect = selectionRect;
            layerRect.x += layerRect.width*2/3;
            DrawLayerTitle(layerRect, LayerMask.LayerToName(gameObject.layer));
        }

        private static void DrawTagTitle(Rect rect, string tag)
        {
            DrawTitle(rect, tag, _tagsSettings.GetColorForTag(tag));
        }

        private static void DrawLayerTitle(Rect rect, string layer)
        {
            DrawTitle(rect, layer, _tagsSettings.GetColorForLayer(layer));
        }

        private static void DrawTitle(Rect rect, string title, Color color)
        {
            var style = new GUIStyle(EditorStyles.boldLabel);
            style.normal.textColor = color;
            EditorGUI.LabelField(rect, title, style);
        }
    }
}