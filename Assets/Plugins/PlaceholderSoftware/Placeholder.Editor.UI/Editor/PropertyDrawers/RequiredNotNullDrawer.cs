using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Helpers;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(RequiredNotNullAttribute))]
    public class RequiredNotNullDrawer
        : PropertyDrawer
    {
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, [NotNull] GUIContent label)
        {
            var field = (RequiredNotNullAttribute)attribute;

            var c = property.objectReferenceValue == null
                ? field.Error
                : field.OkColor;

            using (new ColorScope(c.ToColor()))
                EditorGUI.PropertyField(position, property, label);
        }
    }
}
