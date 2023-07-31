#nullable enable

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// Displays a single SerializableProperty
    /// </summary>
    public class Property
        : IComponent
    {
        private readonly string _name;
        private readonly string? _label;
        private SerializedProperty? _property;

        public bool IsVisible => true;
        public bool RequiresConstantRepaint => false;

        public Property(string name, string? label = null)
        {
            _name = name;
            _label = label;
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
            {
                if (_label == null)
                    EditorGUILayout.PropertyField(_property);
                else
                    EditorGUILayout.PropertyField(_property, new GUIContent(_label, _property.tooltip));
            }
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            if (_property != null)
                yield return _property;
        }
    }
}
