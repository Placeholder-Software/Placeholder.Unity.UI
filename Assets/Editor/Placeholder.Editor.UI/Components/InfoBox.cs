using System;
using System.Collections.Generic;
using UnityEditor;

namespace Assets.Editor.Placeholder.Editor.UI.Components
{
    /// <summary>
    /// Display an info box with a message
    /// </summary>
    public class InfoBoxComponent
        : IComponent
    {
        private readonly string _message;
        private readonly MessageType _type;

        public bool IsVisible => true;

        public InfoBoxComponent(string message, MessageType type)
        {
            _message = message;
            _type = type;
        }

        public void OnEnable(SerializedObject target)
        {
        }

        public void Draw()
        {
            EditorGUILayout.HelpBox(_message, _type);
            EditorGUILayout.Space(2);
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            return Array.Empty<SerializedProperty>();
        }
    }
}
