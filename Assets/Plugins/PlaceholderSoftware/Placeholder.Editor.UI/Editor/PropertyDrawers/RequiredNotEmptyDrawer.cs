using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Helpers;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyValidators;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(RequireNotEmptyAttribute))]
    public class RequiredNotEmptyDrawer
        : PropertyDrawer
    {
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, [NotNull] GUIContent label)
        {
            var field = (RequireNotEmptyAttribute)attribute;

            var c = IsValid(property)
                ? field.OkColor
                : field.Error;

            using (new ColorScope(c.ToColor()))
                EditorGUI.PropertyField(position, property, label);
        }

        private static bool IsValid(SerializedProperty property)
        {
            var err = new RequireNotEmptyValidator().TryGetError(null, property);
            return err == null;
        }
    }
}
