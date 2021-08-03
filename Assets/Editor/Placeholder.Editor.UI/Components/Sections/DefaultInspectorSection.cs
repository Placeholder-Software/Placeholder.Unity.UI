using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    internal class DefaultInspectorSection
        : BaseFoldoutSection
    {
        private UnityEditor.Editor _editor;

        protected override GUIContent Title => new GUIContent("Default Inspector");

        protected override void DrawContent()
        {
            if (_editor == null)
                _editor = UnityEditor.Editor.CreateEditor(Target.targetObjects);
            _editor.DrawDefaultInspector();
        }

        public override IEnumerable<SerializedProperty> GetChildProperties()
        {
            return Array.Empty<SerializedProperty>();
        }
    }
}

