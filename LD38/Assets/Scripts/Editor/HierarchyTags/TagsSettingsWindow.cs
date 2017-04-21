using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.HierarchyTags
{
    class TagsSettingsWindow : EditorWindow
    {
        private const string Title = "Tags settings window";
        private TagsSettings _tagsSettings;
        private int _selectedMode;
        
        [MenuItem("Window/Tags settings window")]
        private static void ShowSelf()
        {
            GetWindow<TagsSettingsWindow>(false, Title);
        }

        private void OnFocus()
        {
            _tagsSettings = HierarchyTags.TagsSettings;
        }

        protected virtual void OnGUI()
        {
            DrawPreview();
            DrawLayoutSettings();
            if (_selectedMode == 0) DrawTagsSettingsEditor();
            else DrawLayersSettingsEditor();
            EditorUtility.SetDirty(_tagsSettings);
        }

        private void DrawPreview()
        {
            _selectedMode = EditorGUILayout.Popup(_selectedMode, new[] {"Tags", "Layers"}, EditorStyles.toolbarPopup);
        }

        private void DrawLayoutSettings()
        {
            
        }

        private void DrawTagsSettingsEditor()
        {
            EditorGUILayout.LabelField("Tags settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            for (var index = 0; index < _tagsSettings.Tags.Length; index++)
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                EditorGUILayout.LabelField(_tagsSettings.Tags[index].Name);
                _tagsSettings.Tags[index].Color = EditorGUILayout.ColorField("Color", _tagsSettings.Tags[index].Color);
                EditorGUILayout.EndHorizontal();
            }
        }
        private void DrawLayersSettingsEditor()
        {
            EditorGUILayout.LabelField("Layers settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            for (var index = 0; index < _tagsSettings.Layers.Length; index++)
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                EditorGUILayout.LabelField(_tagsSettings.Layers[index].Name);
                _tagsSettings.Layers[index].Color = EditorGUILayout.ColorField("Color", _tagsSettings.Layers[index].Color);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
