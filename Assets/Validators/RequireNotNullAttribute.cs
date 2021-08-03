using System;
using UnityEngine;

namespace Validators
{
    /// <summary>
    /// Mark a property as required to be non-null. If null, it will be highlighted in the inspector.
    /// </summary>
    public class RequiredNotNullAttribute
        : BaseValidationAttribute
    {
        public HighlightColor ErrorColor { get; }
        public HighlightColor OkColor { get; }
        public bool Validate { get; }

        public RequiredNotNullAttribute(HighlightColor err = HighlightColor.Error, HighlightColor ok = HighlightColor.None, bool validate = true)
        {
            ErrorColor = err;
            OkColor = ok;
            Validate = validate;
        }
    }

    public enum HighlightColor
    {
        None,
        Error,
        Warn,
        OkGreen,
        OkBlue,
    }

    public static class HighlightColorExtensions
    {
        public static Color ToColor(this HighlightColor highlight)
        {
            switch (highlight)
            {
                case HighlightColor.None:
                    return GUI.color;
                case HighlightColor.Error:
                    return Color.red;
                case HighlightColor.OkGreen:
                    return Color.green;
                case HighlightColor.OkBlue:
                    return Color.blue;
                case HighlightColor.Warn:
                    return Color.yellow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(highlight), highlight, null);
            }
        }
    }
}