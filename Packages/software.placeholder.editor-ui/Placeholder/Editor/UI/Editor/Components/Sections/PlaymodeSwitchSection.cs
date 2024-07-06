#nullable enable

using System.Collections.Generic;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Components.Sections
{
    public class PlaymodeSwitchSection
        : IComponent
    {
        public bool IsVisible => true;
        public bool RequiresConstantRepaint => Child.RequiresConstantRepaint;

        private BasePlaceholderEditor? _editor;

        private readonly IComponent _playmode;
        private readonly IComponent _nonPlaymode;
        private IComponent Child => EditorApplication.isPlaying ? _playmode : _nonPlaymode;

        public BasePlaceholderEditor? Editor
        {
            get => _editor;
            set
            {
                _editor = value;
                _playmode.Editor = value;
                _nonPlaymode.Editor = value;
            }
        }

        public PlaymodeSwitchSection(IComponent playmode, IComponent nonPlaymode)
        {
            _playmode = playmode;
            _nonPlaymode = nonPlaymode;
        }

        public void OnEnable(SerializedObject target)
        {
            _playmode.OnEnable(target);
            _nonPlaymode.OnEnable(target);
        }

        public void OnDisable()
        {
            _playmode.OnDisable();
            _nonPlaymode.OnDisable();
        }

        public void Draw()
        {
            Child.Draw();
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            return Child.GetChildProperties();
        }
    }
}