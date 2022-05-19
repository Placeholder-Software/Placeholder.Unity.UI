using System.Collections.Generic;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components.Sections
{
    /// <summary>
    /// Only draws children in playmode
    /// </summary>
    public class PlaymodeSection
        : IComponent
    {
        private readonly IComponent _child;

        public bool IsVisible => EditorApplication.isPlaying;

        public PlaymodeSection(IComponent child)
        {
            _child = child;
        }

        public void OnEnable(SerializedObject target)
        {
            _child.OnEnable(target);
        }

        public void Draw()
        {
            _child.Draw();
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            return _child.GetChildProperties();
        }
    }
}
