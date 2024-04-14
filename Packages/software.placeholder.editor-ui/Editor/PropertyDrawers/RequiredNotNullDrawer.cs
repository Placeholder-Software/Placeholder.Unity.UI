using JetBrains.Annotations;
using Placeholder.Editor.UI.Core.Validators;
using Placeholder.Editor.UI.Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(RequiredNotNullAttribute))]
    public class RequiredNotNullDrawer
        : PropertyDrawer
    {
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, [NotNull] GUIContent label)
        {
            var field = (RequiredNotNullAttribute)attribute;

            var c = IsValid(property)
                ? field.OkColor
                : field.Error;

            using (new ColorScope(c.ToColor()))
                EditorGUI.PropertyField(position, property, label);
        }

        private static bool IsValid(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.String)
                return property.stringValue != null;

            return property.objectReferenceValue != null;
        }
    }
}
