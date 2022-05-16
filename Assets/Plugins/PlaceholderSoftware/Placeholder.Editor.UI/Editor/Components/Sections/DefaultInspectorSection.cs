using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components.Sections
{
    /// <summary>
    /// A foldout section which draws the default Unity inspector within
    /// </summary>
    public class DefaultInspectorSection
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

