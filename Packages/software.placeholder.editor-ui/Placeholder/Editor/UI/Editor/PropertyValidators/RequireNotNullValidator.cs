#nullable enable

using System;
using JetBrains.Annotations;
using Placeholder.Editor.UI.Core.Validators;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.PropertyValidators
{
    [PropertyValidator(typeof(RequiredNotNullAttribute))]
    public class RequireNotNullValidator
        : IPropertyValidator
    {
        public string? TryGetError(Attribute attribute, SerializedProperty property)
        {
            return TryGetError(property, property.name);
        }

        private string? TryGetErrorArray([NotNull] SerializedProperty arrayProperty, string name)
        {
            for (var i = 0; i < arrayProperty.arraySize; i++)
            {
                var idx = arrayProperty.GetArrayElementAtIndex(i);
                var err = TryGetError(idx, $"{name}[{i}]");
                if (err != null)
                    return err;
            }

            return null;
        }

        public string? TryGetError(SerializedProperty property, string name)
        {
            if (property.propertyType == SerializedPropertyType.String)
                if (property.stringValue != null)
                    return null;

            if (property.isArray)
                return TryGetErrorArray(property, name);
            
            if (property.objectReferenceValue != null)
                return null;
            

            return $"`{name}` must not be null";
        }
    }
}
