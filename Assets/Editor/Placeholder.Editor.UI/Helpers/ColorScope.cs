using System;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Helpers
{
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
