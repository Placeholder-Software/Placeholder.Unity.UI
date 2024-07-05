#nullable enable

using System.Collections.Generic;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// Show one single property from a set based on the value of descriminator property
    /// </summary>
    public class PropertySelector
        : IComponent
    {
        private readonly string _discriminatorPropertyName;
        private readonly IReadOnlyDictionary<int, IComponent> _components;

        private SerializedProperty? _discriminatorProperty;

        public bool RequiresConstantRepaint => GetCurrentComponent()?.RequiresConstantRepaint ?? false;
        public bool IsVisible => true;

        private BasePlaceholderEditor? _editor;
        public BasePlaceholderEditor? Editor
        {
            get => _editor;
            set
            {
                _editor = value;
                foreach (var component in _components)
                    component.Value.Editor = value;
            }
        }

        public PropertySelector(string discriminatorProperty, IReadOnlyDictionary<int, IComponent> components)
        {
            _discriminatorPropertyName = discriminatorProperty;
            _components = components;
        }

        public void OnEnable(SerializedObject target)
        {
            _discriminatorProperty = target.FindProperty(_discriminatorPropertyName);

            foreach (var component in _components)
                component.Value.OnEnable(target);
        }

        public void OnDisable()
        {
            foreach (var component in _components)
                component.Value.OnDisable();
        }

        public void Draw()
        {
            GetCurrentComponent()?.Draw();
        }

        private IComponent? GetCurrentComponent()
        {
            if (_discriminatorProperty == null)
                return null;

            var value = _discriminatorProperty.intValue;
            return _components.GetValueOrDefault(value);
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            if (_discriminatorProperty == null)
                yield break;

            var value = _discriminatorProperty.intValue;
            if (!_components.TryGetValue(value, out var component) || component == null)
                yield break;
            
            foreach (var prop in component.GetChildProperties())
                yield return prop;
        }
    }
}
