using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Helpers;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyValidators;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components.Addons.Validation
{
    /// <summary>
    /// Automatically generates error messages based on attributes attached to properties
    /// </summary>
    public class AttributeValidation
        : IValidation
    {
        private Cache _cache;

        public string TryGetErrorMessage(SerializedObject target, IComponent parent)
        {
            if (_cache == null || _cache.IsStale(target, parent))
                _cache = new Cache(target, parent);
            return _cache.TryGetErrorMessage();
        }

        private class Cache
        {
            [NotNull] private readonly SerializedObject _target;
            [NotNull] private readonly IComponent _parent;
            [NotNull] private Dictionary<Type, IPropertyValidator> _validators;

            private readonly IReadOnlyList<(SerializedProperty property, IValidationAttribute attribute)> _attributes;

            public Cache([NotNull] SerializedObject target, [NotNull] IComponent parent)
            {
                _target = target;
                _parent = parent;

                var attrs = from property in parent.GetChildProperties()
                            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                            where property != null
                            from attribute in property.GetCustomAttributes<IValidationAttribute>()
                            select (property, attribute);
                _attributes = attrs.ToList();
            }

            [CanBeNull] public string TryGetErrorMessage()
            {
                // Find all errors from attributes
                var count = 0;
                string error = null;
                foreach (var (prop, attr) in _attributes)
                {
                    var val = ValidatorLocator.FindValidator(attr.GetType());

                    var err = val?.TryGetError((Attribute)attr, prop);
                    if (err == null)
                        continue;

                    count++;

                    if (error == null)
                        error = err;
                }

                if (count == 0)
                    return null;
                if (count == 1)
                    return error;

                if (count == 2)
                    return $"{error}\n(1 other error)";

                return $"{error}\n({count - 1} other errors)";
            }

            public bool IsStale([NotNull] SerializedObject target, [NotNull] IComponent parent)
            {
                return _target != target || _parent != parent;
            }
        }
    }
}
