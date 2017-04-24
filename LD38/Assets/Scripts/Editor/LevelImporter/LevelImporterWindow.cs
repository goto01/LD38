using System.IO;
using System.Linq;
using Assets.Scripts.Components.Level;
using Assets.Scripts.Controllers;
using Mono.CSharp;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.LevelImporter
{
    class LevelImporterWindow : EditorWindow
    {
        private const string Ext = ".json";
        private string _message;
        private Location _location;

        [MenuItem("Window/Level importer")]
        public static void ShowSelf()
        {
            GetWindow<LevelImporterWindow>();
        }

        protected virtual void OnGUI()
        {
            _location = EditorGUILayout.ObjectField("Location prefab", _location, typeof (Location), false) as Location;
            if (_location == null) return;
            var boxRect = GUILayoutUtility.GetRect(0, 30);
            GUI.Box(boxRect, "Drag and drop json file here");
            switch (Event.current.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!boxRect.Contains(Event.current.mousePosition)) break;
                    if (!DragAndDrop.paths[0].Contains(Ext)) break;
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (Event.current.type == EventType.DragPerform)
                    {
                        var data = ReadJson(DragAndDrop.paths[0]);
                        var map = JsonUtility.FromJson<Map>(data);
                        var layer = map.Layers[0];
                        var enemyLayer = map.Layers[1];
                        var doorLayer = map.Layers[2];
                        var contructor = new LocationContructor(_location);
                        contructor.GenerateLocation(layer.Width, layer.Height, layer.Data, enemyLayer.Data,
                            doorLayer.Data);
                        _message = string.Format("Map importer with size : width {0}, height {1}", layer.Width,
                            layer.Height);
                    }
                    break;
            }
        }

        private string ReadJson(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
