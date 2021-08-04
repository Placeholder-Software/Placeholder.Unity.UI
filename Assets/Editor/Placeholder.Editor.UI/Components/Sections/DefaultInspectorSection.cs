using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    internal class DefaultInspectorSection
        : FoldoutSection
    {
        private UnityEditor.Editor _editor;

        public DefaultInspectorSection()
            : base(new GUIContent("Default Inspector"), new ComponentList())
        {
        }

        protected override void DrawContent()
        {
            if (_editor == null)
                _editor = UnityEditor.Editor.CreateEditor(Target.targetObjects);
            _editor.DrawDefaultInspector();
        }
    }
}

