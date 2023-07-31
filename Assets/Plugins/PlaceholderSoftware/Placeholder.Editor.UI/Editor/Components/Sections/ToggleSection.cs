#nullable enable

using Placeholder.Editor.UI.Editor.Helpers;
using Placeholder.Editor.UI.Editor.Style;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components.Sections
{
    public class ToggleSection
        : BaseSection
    {
        private readonly string _enabledPropertyName;
        private SerializedProperty? _enabled;

        protected override bool Enabled
        {
            get => _enabled?.boolValue ?? false;
            set => _enabled!.boolValue = value;
        }

        public ToggleType ToggleType { get; set; } = ToggleType.Tick;

        public ToggleSection(GUIContent title, string enabledPropertyName, IComponent component)
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