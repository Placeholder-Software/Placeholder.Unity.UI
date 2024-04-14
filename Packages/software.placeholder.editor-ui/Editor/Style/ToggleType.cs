using System;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Style
{
    public enum ToggleType
    {
        /// <summary>
        /// A box with a toggleable tick
        /// </summary>
        Tick,

        /// <summary>
        /// A box with a bar through it (non-toggleable)
        /// </summary>
        Mixed,

        /// <summary>
        /// An "S" in a box, colour changes when toggled
        /// </summary>
        Solo,

        /// <summary>
        /// An "M" in a box, colour changes when toggled
        /// </summary>
        Mute,

        /// <summary>
        /// A "B" in a box, colour changes when toggled
        /// </summary>
        Bypass,
    }

    public static class ToggleTypeExtensions
    {
        public static GUIStyle ToStyle(this ToggleType type)
        {
            switch (type)
            {
                case ToggleType.Tick:
                    return Styles.HeaderTickToggle;
                case ToggleType.Mixed:
                    return Styles.HeaderMixedToggle;
                case ToggleType.Solo:
                    return Styles.HeaderSoloToggle;
                case ToggleType.Mute:
                    return Styles.HeaderMuteToggle;
                case ToggleType.Bypass:
                    return Styles.HeaderBypassToggle;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
