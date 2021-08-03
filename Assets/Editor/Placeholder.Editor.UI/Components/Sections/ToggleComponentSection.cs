using System.Collections.Generic;
using Assets.Editor.Placeholder.Editor.UI.Style;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    public class ToggleComponentSection
        : BaseToggleSection
    {
        private readonly IComponent _component;

        protected override GUIContent Title { get; }

        public ToggleComponentSection([NotNull] GUIContent title, string enabledProperty, ToggleType type, [NotNull] IComponent component)
            : base(enabledProperty, type)
        {
            Title = title;
            _component = component;
        }

        public ToggleComponentSection([NotNull] GUIContent title, string enabledProperty, ToggleType type, [NotNull, ItemNotNull] params IComponent[] components)
            : base(enabledProperty, type)
        {
            Title = title;
            _component = new ComponentList(components: components);
        }

        public override void OnEnable(SerializedObject target)
        {
            base.OnEnable(target);

            _component.OnEnable(target);
        }

        protected override void DrawContent()
        {
            _component.Draw();
        }

        public override IEnumerable<SerializedProperty> GetChildProperties()
        {
            return _component.GetChildProperties();
        }
    }
}
