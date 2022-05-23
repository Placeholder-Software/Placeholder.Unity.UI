using System;
using System.Collections.Generic;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components.Sections
{
    /// <summary>
    /// Disable children based on a boolean property
    /// </summary>
    public class DisabledSection
        : IComponent
    {
        private readonly string _controlProperty;
        private readonly IComponent _child;

        private SerializedProperty _control;

        public bool IsVisible => true;
        public bool RequiresConstantRepaint => _child.RequiresConstantRepaint;

        public DisabledSection(string controlProperty, IComponent child)
        {
            _controlProperty = controlProperty;
            _child = child;
        }

        public void OnEnable(SerializedObject target)
        {
            _control = target.FindProperty(_controlProperty);
            _child.OnEnable(target);
        }

        public void Draw()
        {
            using (new EditorGUI.DisabledScope(!_control.boolValue))
                _child.Draw();
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            if (_control.boolValue)
                return _child.GetChildProperties();

            return Array.Empty<SerializedProperty>();
        }
    }
}
