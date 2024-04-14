#nullable enable

using System;
using JetBrains.Annotations;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.PropertyValidators
{
    public interface IPropertyValidator
    {
        string? TryGetError(Attribute attribute, [NotNull] SerializedProperty property);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PropertyValidatorAttribute
        : Attribute
    {
        public Type ValidatorFor { get; }

        public PropertyValidatorAttribute(Type validatorFor)
        {
            ValidatorFor = validatorFor;
        }
    }
}
