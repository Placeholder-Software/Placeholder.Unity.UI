using System;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.PropertyDrawers;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.PropertyDrawers
{
    /// <summary>
    /// Draw a slider with two ends, to set a min and max value (in X/Y fields of a vector2)
    /// </summary>
    /// <remarks>based on https://gist.github.com/LotteMakesStuff/0de9be35044bab97cbe79b9ced695585
    /// </remarks>
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer
        : PropertyDrawer
    {
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, GUIContent label)
        {
            // cast the attribute to make life easier
            var minMax = (MinMaxAttribute)attribute;

            // Sanity check field type
            if (property.propertyType != SerializedPropertyType.Vector2)
                throw new NotSupportedException("[MinMaxAttribute] must be applied on a Vector2");

            // the currently set minimum and maximum value
            var minValue = property.vector2Value.x;
            var maxValue = property.vector2Value.y;

            // the limit for both min and max, min cant go lower than minLimit and max can't tgo aboveop maxLimit
            var minLimit = minMax.MinLimit;
            var maxLimit = minMax.MaxLimit;

            // If the size is too narrow or values are hidden skip drawing the value label
            if (position.width < 100 || !minMax.ShowValues)
            {
                minValue = Math.Max(minLimit, minValue);
                maxValue = Math.Min(maxLimit, maxValue);

                EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, minLimit, maxLimit);
            }
            else
            {
                var vwidth = Mathf.Clamp(position.width * 0.1f, 30, 70);
                var slider = new Rect(position.position, new Vector2(position.width - vwidth * 2 - 4, position.height));

                var value1 = new Rect(new Vector2(position.x + position.width - vwidth * 2 - 2, position.y), new Vector2(vwidth, position.height));
                var value2 = new Rect(new Vector2(position.x + position.width - vwidth, position.y), new Vector2(vwidth, position.height));

                EditorGUI.MinMaxSlider(slider, label, ref minValue, ref maxValue, minLimit, maxLimit);

                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                minValue = EditorGUI.FloatField(value1, minValue);
                maxValue = EditorGUI.FloatField(value2, maxValue);
                EditorGUI.indentLevel = indent;
            }

            // save the results into the property!
            property.vector2Value = new Vector2(minValue, maxValue);
        }
    }
}