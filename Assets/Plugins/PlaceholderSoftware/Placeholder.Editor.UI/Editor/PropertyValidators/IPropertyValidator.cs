using System;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyValidators
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
