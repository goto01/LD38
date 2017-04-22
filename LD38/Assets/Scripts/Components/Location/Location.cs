using Assets.Scripts.Core.Staff;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.Components.Location
{
    class Location : BindingMonoBehaviour
    {
        [SerializeField] private GameObject _borderTile;
        [SerializeField] private int _tileWidth = 18;
        [SerializeField] private int _tileHeight = 18;
        [SerializeField] private int _width = 16;
        [SerializeField] private int _height = 16;
        [SerializeField] private float _offsetX = .03125f;
        [SerializeField] private float _offsetY = .03125f;

        public void GenerateLocation()
        {
            for (var index = 0; index < _width; index++)
            {
                var tile = Instantiate(_borderTile);
                var x = ((float) index - (float) _width/2)*_tileWidth*0.03125f;
                var y = ((float) _height/2)*_tileHeight*0.03125f;
                var pos = new Vector3(x, y, tile.transform.position.z);
                tile.transform.position = pos;
                tile.transform.parent = transform;
            }
            for (var index = 0; index < _width; index++)
            {
                var tile = Instantiate(_borderTile);
                var x = ((float) index - (float) _width/2)*_tileWidth*0.03125f;
                var y = -((float) _height/2)*_tileHeight*0.03125f;
                var pos = new Vector3(x, y, tile.transform.position.z);
                tile.transform.position = pos;
                tile.transform.parent = transform;
            }
            for (var index = 1; index < _height; index++)
            {
                var tile = Instantiate(_borderTile);
                var x = ((float) _width/2 - 1)*_tileWidth*0.03125f;
                var y = ((float) index - (float) _height/2)*_tileHeight * 0.03125f;
                var pos = new Vector3(x, y, tile.transform.position.z);
                tile.transform.position = pos;
                tile.transform.parent = transform;
            }
            for (var index = 1; index < _height; index++)
            {
                var tile = Instantiate(_borderTile);
                var x = -((float)_width / 2) * _tileWidth * 0.03125f;
                var y = ((float)index - (float)_height / 2) * _tileHeight * 0.03125f;
                var pos = new Vector3(x, y, tile.transform.position.z);
                tile.transform.position = pos;
                tile.transform.parent = transform;
            }
        }

        public void Clear()
        {
            var cildCount = transform.childCount;
            for (var index = 0; index < cildCount; index++) DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof (Location))]
    class LocationEditor : BindingMonoBehaviourEditor
    {
        private Location Target { get { return target as Location; } }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Generate")) Target.GenerateLocation();
            if (GUILayout.Button("Clear")) Target.Clear();
        }
    }

#endif
}
