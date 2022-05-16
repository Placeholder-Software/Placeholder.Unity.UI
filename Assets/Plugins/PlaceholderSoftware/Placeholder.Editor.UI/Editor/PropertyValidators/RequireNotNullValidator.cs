using System;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyValidators
{
    [PropertyValidator(typeof(RequiredNotNullAttribute))]
    public class RequireNotNullValidator
        : IPropertyValidator
    {
        public string TryGetError(Attribute attribute, SerializedProperty property)
        {
            return TryGetError(attribute, property, property.name);
        }

        [CanBeNull] private string TryGetErrorArray(RequiredNotNullAttribute attr, [NotNull] SerializedProperty arrayProperty, string name)
        {
            for (var i = 0; i < arrayProperty.arraySize; i++)
            {
                var idx = arrayProperty.GetArrayElementAtIndex(i);
                var err = TryGetError(attr, idx, $"{name}[{i}]");
                if (err != null)
                    return err;
            }

            return null;
        }

        [CanBeNull] public string TryGetError(Attribute attribute, SerializedProperty property, string name)
        {
            var attr = (RequiredNotNullAttribute)attribute;
            if (!attr.Validate)
                return null;

            if (property.isArray)
                return TryGetErrorArray(attr, property, name);

            if (property.objectReferenceValue != null)
                return null;

            return $"`{name}` must not be null";
        }
    }
}
