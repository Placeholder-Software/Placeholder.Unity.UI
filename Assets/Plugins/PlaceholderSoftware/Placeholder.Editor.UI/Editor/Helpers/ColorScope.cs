using System;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Helpers
{
    /// <summary>
    /// Set `GUI.color` for the duration of this scope
    /// </summary>
    internal readonly struct ColorScope
        : IDisposable
    {
        private readonly Color _saved;

        public ColorScope(Color color)
        {
            _saved = GUI.color;
            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = _saved;
        }
    }
}
