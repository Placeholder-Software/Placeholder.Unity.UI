using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Helpers
{
    public static class SerializedPropertyExtensions
    {
        /// <summary>
        /// Reflect out the `FieldInfo` object behind the given SerializedProperty
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [CanBeNull] public static FieldInfo ResolveFieldInfo([NotNull] this SerializedProperty property)
        {
            FieldInfo field = null;

            var path = property.propertyPath.Split('.');
            var type = property.serializedObject.targetObject.GetType();

            foreach (var part in path)
            {
                field = type.GetField(part, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field == null)
                    return null;

                type = field.FieldType;
            }

            return field;
        }

        /// <summary>
        /// Get a custom attribute attached to the field of the given SerializedProperty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        [CanBeNull] public static T GetCustomAttribute<T>([NotNull] this SerializedProperty property)
            where T : Attribute
        {
            var field = property.ResolveFieldInfo();
            if (field == null)
                return null;

            return field.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Get custom attributes attached to the field of the given SerializedProperty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        [NotNull] public static IEnumerable<T> GetCustomAttributes<T>([NotNull] this SerializedProperty property)
        {
            var field = property.ResolveFieldInfo();
            if (field == null)
                return Array.Empty<T>();

            return field.GetCustomAttributes().OfType<T>();
        }
    }
}
