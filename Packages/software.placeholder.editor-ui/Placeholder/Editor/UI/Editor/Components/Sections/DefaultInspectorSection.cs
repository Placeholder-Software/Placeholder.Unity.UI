using Placeholder.Editor.UI.Editor.Components;
using Placeholder.Editor.UI.Editor.Components.Sections;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components.Section
{
    /// <summary>
    /// A foldout section which draws the default Unity inspector within
    /// </summary>
    public class DefaultInspectorSection
        : FoldoutSection
    {
        public DefaultInspectorSection()
            : base(new GUIContent("Default Inspector"), new ComponentList())
        {
        }

        protected override void DrawContent()
        {
            if (Editor == null)
                EditorGUILayout.HelpBox("No editor instance!", MessageType.Error);
            else
                Editor.DrawDefaultInspector();
        }
    }
}