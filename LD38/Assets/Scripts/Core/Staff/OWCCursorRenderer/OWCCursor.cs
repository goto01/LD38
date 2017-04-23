using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.Core.Staff.OWCCursorRenderer
{
    [Serializable]
    public class OWCCursor : ScriptableObject
    {
        #region Fields

        [Space]
        [SerializeField] private Texture2D _texture;
        [SerializeField] private Vector2 _offset;

        #endregion

        #region Properties

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Vector2 TextureSize { get { return _texture == null ? Vector2.zero : new Vector2(_texture.width, _texture.height); } }

        public Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        #endregion

#if UNITY_EDITOR

        [MenuItem("Assets/Create/Create cursor")]
        public static void CreateSelf()
        {
            var asset = CreateInstance<OWCCursor>();
            ProjectWindowUtil.CreateAsset(asset, string.Format("{0}.asset", "Cursor"));
        }
#endif
    }
}
