using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// Displays a single SerializableProperty
    /// </summary>
    public class Property
        : IComponent
    {
        private readonly string _name;
        private SerializedProperty _property;

        public bool IsVisible => true;

        public Property(string name)
        {
            _name = name;
        }

        public void OnEnable(SerializedObject target)
        {
            _property = target.FindProperty(_name);
        }

        public void Draw()
        {
            if (_property == null)
            {
                var saved = GUI.color;
                GUI.color = Color.magenta;
                EditorGUILayout.LabelField($"Cannot find property `{_name}`");
                GUI.color = saved;
            }
            else
                EditorGUILayout.PropertyField(_property);
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            yield return _property;
        }
    }
}
