#nullable enable

using System;
using System.Collections.Generic;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Components.Sections
{
    /// <summary>
    /// Disable children based on a boolean property
    /// </summary>
    public class DisabledSection
        : IComponent
    {
        private readonly string _controlProperty;
        private readonly IComponent _child;

        private SerializedProperty? _control;

        public bool IsVisible => true;
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

        public void OnDisable()
        {
            _child.OnDisable();
        }

        public void Draw()
        {
            if (_control == null)
                return;

            using (new EditorGUI.DisabledScope(!_control.boolValue))
                _child.Draw();
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            if (_control is { boolValue: true })
                return _child.GetChildProperties();

            return Array.Empty<SerializedProperty>();
        }
    }
}
