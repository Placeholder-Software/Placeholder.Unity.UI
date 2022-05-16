using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.PropertyDrawers
{
    /// <summary>
    /// Shows a range slider on the inspector, allowing the user to set min and max values.
    /// Must be applied to a `Vector2` field.
    /// `x` is the min value. `y` is the max value.
    /// </summary>
    public class MinMaxAttribute
        : PropertyAttribute
    {
        public float MinLimit { get; }
        public float MaxLimit { get; }
        public readonly bool ShowValues;

        public MinMaxAttribute(int min, int max, bool showValues = true)
        {
            MinLimit = min;
            MaxLimit = max;
            ShowValues = showValues;
        }
    }
}
