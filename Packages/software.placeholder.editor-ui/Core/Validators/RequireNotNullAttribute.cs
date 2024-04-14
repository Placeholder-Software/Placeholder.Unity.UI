#nullable enable

using System;
using UnityEngine;

namespace Placeholder.Editor.UI.Core.Validators
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
            return highlight switch
            {
                Highlight.None => GUI.color,
                Highlight.Error => Color.red,
                Highlight.OkGreen => Color.green,
                Highlight.OkBlue => Color.blue,
                Highlight.Warn => Color.yellow,
                _ => throw new ArgumentOutOfRangeException(nameof(highlight), highlight, null)
            };
        }
    }
}