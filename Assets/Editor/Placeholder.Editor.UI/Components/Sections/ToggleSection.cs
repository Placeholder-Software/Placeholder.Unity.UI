using Assets.Editor.Placeholder.Editor.UI.Helpers;
using Assets.Editor.Placeholder.Editor.UI.Style;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    public class ToggleSection
        : BaseSection
    {
        private readonly string _enabledPropertyName;
        private SerializedProperty _enabled;

        protected override bool Enabled
        {
            get => _enabled.boolValue;
            set => _enabled.boolValue = value;
        }

        public ToggleType ToggleType { get; set; } = ToggleType.Tick;

        public ToggleSection([NotNull] GUIContent title, string enabledPropertyName, IComponent component)
            : base(title, component)
        {
            _enabledPropertyName = enabledPropertyName;
        }

        public override void OnEnable(SerializedObject target)
        {
            base.OnEnable(target);

            _enabled = target.FindProperty(_enabledPropertyName);
        }

        protected override bool DrawHeader(GUIContent title, GenericMenu menu, string helpUrl, string errorMessage)
        {
            var expanded = Expanded;
            var enabled = Enabled;

            Header.Toggle(title, ref enabled, ref expanded, ToggleType, new Header.HeaderOptions {
                DetailMenu = menu,
                HelpUrl = helpUrl,
                ErrorMessage = errorMessage
            });

            Enabled = enabled;
            return expanded;
        }
    }
}