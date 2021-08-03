using System;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Editor.Placeholder.Editor.UI.PropertyValidators
{
    public interface IPropertyValidator
    {
        [CanBeNull] string TryGetError(Attribute attribute, [NotNull] SerializedProperty property);
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
