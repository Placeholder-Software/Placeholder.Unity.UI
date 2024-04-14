using System;
using UnityEngine;

namespace Placeholder.Editor.UI.Core.Validators
{
    /// <summary>
    /// Attribute used to mark a field with custom validation logic which can be used in the inspector
    /// to show warnings when a value is invalid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class BaseValidationAttribute
        : PropertyAttribute, IValidationAttribute
    {
    }

    public interface IValidationAttribute
    {
    }
}
