using System;
using UnityEditor;
using Validators;

namespace Assets.Editor.Placeholder.Editor.UI.PropertyValidators
{
    [PropertyValidator(typeof(IsOddAttribute))]
    public class IsOddValidator
        : IPropertyValidator
    {
        public string TryGetError(Attribute attribute, SerializedProperty property)
        {
            double value;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    value = property.intValue;
                    break;
                case SerializedPropertyType.Float:
                    value = property.doubleValue;
                    break;
                default:
                    return $"`IsOdd` invalid for field `{property.name}` of type `{property.propertyType}`";
            }

            if (value % 2 == 0)
                return $"`{property.name}` must be odd";

            return null;
        }
    }
}
