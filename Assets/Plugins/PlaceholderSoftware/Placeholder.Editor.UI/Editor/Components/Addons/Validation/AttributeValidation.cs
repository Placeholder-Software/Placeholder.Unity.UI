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

            public Cache([NotNull] SerializedObject target, [NotNull] IComponent parent)
            {
                _target = target;
                _parent = parent;
            }

            private IReadOnlyList<(SerializedProperty property, IValidationAttribute attribute)> GetAttributes([NotNull] IComponent parent)
            {
                var attrs = from property in parent.GetChildProperties()
                            from attribute in property.GetCustomAttributes<IValidationAttribute>()
                            select (property, attribute);
                return attrs.ToList();
            }

            [CanBeNull] public string TryGetErrorMessage()
            {
                var attrs = GetAttributes(_parent);

                // Find all errors from attributes
                var count = 0;
                string error = null;
                foreach (var (prop, attr) in attrs)
                {
                    var val = ValidatorLocator.FindValidator(attr.GetType());

                    var err = val?.TryGetError((Attribute)attr, prop);
                    if (err == null)
                        continue;

                    count++;

                    if (error == null)
                        error = err;
                }

                switch (count)
                {
                    case 0:
                        return null;
                    case 1:
                        return error;
                    case 2:
                        return $"{error}\n(1 other error)";
                    default:
                        return $"{error}\n({count - 1} other errors)";
                }
            }

            public bool IsStale([NotNull] SerializedObject target, [NotNull] IComponent parent)
            {
                return _target != target || _parent != parent;
            }
        }
    }
}
