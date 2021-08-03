using System;
using UnityEngine;

namespace Validators
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
