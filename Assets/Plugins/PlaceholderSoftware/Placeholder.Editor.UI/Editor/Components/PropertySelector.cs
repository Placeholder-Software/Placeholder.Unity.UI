using System.Collections.Generic;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// Show one single property from a set based on the value of descriminator property
    /// </summary>
    public class PropertySelector
        : IComponent
    {
        private readonly string _discriminatorPropertyName;
        private readonly IReadOnlyDictionary<int, IComponent> _components;

        private SerializedProperty _discriminatorProperty;
        

        public bool IsVisible => true;

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

        public void Draw()
        {
            if (_discriminatorProperty == null)
                return;

            var value = _discriminatorProperty.intValue;
            if (!_components.TryGetValue(value, out var component))
                return;

            component.Draw();
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
