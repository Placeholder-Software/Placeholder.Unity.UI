using System;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Core.Validators
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class BaseValidationAttribute
        : PropertyAttribute, IValidationAttribute
    {
    }

    public interface IValidationAttribute
    {
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
