using UnityEngine;

namespace Assets.Scripts.Core.Staff.OWCCursorRenderer
{
    public class CursorRenderer : BindingMonoBehaviour
    {
        #region Fields

        [SerializeField] private OWCCursor _cursor;

        #endregion

        #region Unity events

        protected virtual void Awake()
        {
            ApplyCursor();
        }

        #endregion

        #region Public methods

        public void SetCursor(OWCCursor cursor)
        {
            _cursor = cursor;
            ApplyCursor();
        }

        #endregion

        #region Private methods

        private void ApplyCursor()
        {
            if (_cursor == null) return;
            Cursor.SetCursor(_cursor.Texture, _cursor.Offset, CursorMode.ForceSoftware);
        }

        #endregion
    }
}
