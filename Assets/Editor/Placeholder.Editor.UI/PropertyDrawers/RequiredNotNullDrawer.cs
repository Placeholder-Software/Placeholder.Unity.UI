using Assets.Editor.Placeholder.Editor.UI.Helpers;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Validators;

namespace Assets.Editor.Placeholder.Editor.UI.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(RequiredNotNullAttribute))]
    public class RequiredNotNullDrawer
        : PropertyDrawer
    {
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, [NotNull] GUIContent label)
        {
            var field = (RequiredNotNullAttribute)attribute;

            var c = property.objectReferenceValue == null
                ? field.ErrorColor
                : field.OkColor;

            using (new ColorScope(c.ToColor()))
                EditorGUI.PropertyField(position, property, label);
        }
    }
}
