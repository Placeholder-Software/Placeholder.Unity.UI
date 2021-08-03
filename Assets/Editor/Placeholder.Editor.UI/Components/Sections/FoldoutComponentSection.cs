using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    public class FoldoutComponentSection
        : BaseFoldoutSection
    {
        private readonly IComponent _component;

        protected override GUIContent Title { get; }

        public FoldoutComponentSection([NotNull] GUIContent title, [NotNull] IComponent component)
        {
            Title = title;
            _component = component;
        }

        public FoldoutComponentSection([NotNull] GUIContent title, [NotNull, ItemNotNull] params IComponent[] components)
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
