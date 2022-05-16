using System;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators
{
    /// <summary>
    /// Mark a property as required to be non-null. If null, it will be highlighted in the inspector.
    /// </summary>
    public class RequiredNotNullAttribute
        : BaseValidationAttribute
    {
        public Highlight Error { get; }
        public Highlight OkColor { get; }
        public bool Validate { get; }

        public RequiredNotNullAttribute(Highlight err = Highlight.Error, Highlight ok = Highlight.None, bool validate = true)
        {
            Error = err;
            OkColor = ok;
            Validate = validate;
        }
    }

    public enum Highlight
    {
        None,
        Error,
        Warn,
        OkGreen,
        OkBlue,
    }

    public static class HighlightExtensions
    {
        public static Color ToColor(this Highlight highlight)
        {
            switch (highlight)
            {
                case Highlight.None:
                    return GUI.color;
                case Highlight.Error:
                    return Color.red;
                case Highlight.OkGreen:
                    return Color.green;
                case Highlight.OkBlue:
                    return Color.blue;
                case Highlight.Warn:
                    return Color.yellow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(highlight), highlight, null);
            }
        }
    }
}