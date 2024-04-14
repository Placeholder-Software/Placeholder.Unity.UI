using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Placeholder.Editor.UI.Editor.PropertyValidators
{
    internal static class ValidatorLocator
    {
        private static bool _cacheDirty = true;
        private static readonly Dictionary<Type, IPropertyValidator> ValidatorsCache = new Dictionary<Type, IPropertyValidator>();

        [CanBeNull] public static IPropertyValidator FindValidator<T>()
            where T : Attribute
        {
            return FindValidator(typeof(T));
        }

        [CanBeNull] public static IPropertyValidator FindValidator([NotNull] Type attrType)
        {
            if (_cacheDirty)
            {
                FillCache();
                _cacheDirty = false;
            }

            ValidatorsCache.TryGetValue(attrType, out var validator);
            return validator;
        }

        private static void FillCache()
        {
            ValidatorsCache.Clear();

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToList();

            var validators = from type in types
                             where typeof(IPropertyValidator).IsAssignableFrom(type)
                             let valAttr = type.GetCustomAttribute<PropertyValidatorAttribute>(true)
                             where valAttr != null
                             select (valAttr, type);

            foreach (var (attr, type) in validators)
            {
                var valFor = attr.ValidatorFor;
                var instance = (IPropertyValidator)Activator.CreateInstance(type);
                ValidatorsCache[valFor] = instance;
            }
        }
    }
}
