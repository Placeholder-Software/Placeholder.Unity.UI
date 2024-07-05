using JetBrains.Annotations;
using Placeholder.Editor.UI.Core.Validators;
using Placeholder.Editor.UI.Editor.Helpers;
using Placeholder.Editor.UI.Editor.PropertyValidators;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.PropertyDrawers
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
