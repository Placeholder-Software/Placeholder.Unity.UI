using System;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class BaseValidationAttribute
        : PropertyAttribute, IValidationAttribute
    {
    }

    public interface IValidationAttribute
    {
    }
}
