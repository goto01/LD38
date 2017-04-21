using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Core.PropertyAttributes;
using Assets.Scripts.Core.Staff.Helpers;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace Assets.Scripts.Core.Staff
{
    public abstract class BindingMonoBehaviour : MonoBehaviour
    {
        #region Fields

        protected const float PixelSize = .03125f;

        #endregion

        #region Properties

        private IList<FieldInfo> BindableFields { get { return GetType().GetFieldsByAttributeType<BindingAttribute>(); } }

        #endregion

        #region Unity events

        protected virtual void Reset()
        {
            BindFields();
        }

        #endregion

        #region Public methods

        public void ReBindingFields()
        {
            BindFields();
        }

        public void DropBindingOfFields()
        {
            DropFieldsBinding();
        }

        #endregion

        #region Protected methods

        protected IEnumerator Call(Action action, float delay)
        {
            var coroutine = StartCoroutine(action, delay);
            StartCoroutine(coroutine);
            return coroutine;
        }

        #endregion

        #region Private methods
        
        private void BindFields()
        {
            foreach (var field in BindableFields) BindField(field);
        }

        private void BindField(FieldInfo field)
        {
            var component = GetComponent(field);
            field.SetValue(this, component);
        }

        private Component GetComponent(FieldInfo field)
        {
            var attribute = field.GetAttribute<BindingAttribute>();
            return attribute.IsRequired ? this.GetOrAddIfNotExistComponent(field.FieldType) : GetComponent(field.FieldType);
        }

        private void DropFieldsBinding()
        {
            foreach (var field in BindableFields) DropFieldBinding(field);
        }

        private void DropFieldBinding(FieldInfo field)
        {
            field.SetValue(this, default(Component));
        }

        private Component GetOrAddIfNotExistComponent(Type type)
        {
            var component = GetComponent(type);
            if (component == null) component = gameObject.AddComponent(type);
            return component;
        }

        private IEnumerator StartCoroutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }

        #endregion
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(BindingMonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class BindingMonoBehaviourEditor : Editor
    {
        #region Properties

        private BindingMonoBehaviour Self { get { return target as BindingMonoBehaviour;} }

        #endregion

        #region Overrided methods

        public override void OnInspectorGUI()
        {
            DrawCustomHeader();
            DrawDefaultInspector();
        }

        #endregion

        #region Draw inspector methods

        protected virtual void DrawCustomHeader()
        {
            DrawReBindingEditor();
        }

        private void DrawReBindingEditor()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rebinding", EditorStyles.miniButtonLeft)) Self.ReBindingFields();
            if (GUILayout.Button("Drop bindings", EditorStyles.miniButtonRight)) Self.DropBindingOfFields();
            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
#endif
}
