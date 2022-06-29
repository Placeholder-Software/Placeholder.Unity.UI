using System;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyValidators
{
    [PropertyValidator(typeof(RequireNotEmptyAttribute))]
    public class RequireNotEmptyValidator
        : IPropertyValidator
    {
        public string TryGetError(Attribute attribute, SerializedProperty property)
        {
            if (property.isArray)
            {
                if (property.arraySize == 0)
                    return $"`{property.name}` must not be empty";
                return null;
            }

            if (property.propertyType == SerializedPropertyType.String)
            {
                if (string.IsNullOrEmpty(property.stringValue))
                    return $"`{property.name}` must not be null/empty";
                return null;
            }

            return $"`RequireNotEmpty` invalid for field `{property.name}` of type `{property.propertyType}`";
        }
    }
}
