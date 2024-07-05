#nullable enable

using System.Collections.Generic;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Components.Sections
{
    /// <summary>
    /// Only draws children in playmode
    /// </summary>
    public class PlaymodeSection
        : IComponent
    {
        private readonly IComponent _child;

        public bool IsVisible => EditorApplication.isPlaying;
        public bool RequiresConstantRepaint => _child.RequiresConstantRepaint;

        private BasePlaceholderEditor? _editor;
        public BasePlaceholderEditor? Editor
        {
            get => _editor;
            set
            {
                _editor = value;
                _child.Editor = value;
            }
        }

        public PlaymodeSection(IComponent child)
        {
            _child = child;
        }

        public void OnEnable(SerializedObject target)
        {
            _child.OnEnable(target);
        }

        public void OnDisable()
        {
            _child.OnDisable();
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
