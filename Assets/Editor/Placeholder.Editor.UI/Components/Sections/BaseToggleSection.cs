using Assets.Editor.Placeholder.Editor.UI.Helpers;
using Assets.Editor.Placeholder.Editor.UI.Style;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    public abstract class BaseToggleSection
        : BaseSection
    {
        private readonly string _enabledPropertyName;
        private SerializedProperty _enabled;

        protected override bool Enabled
        {
            get => _enabled.boolValue;
            set => _enabled.boolValue = value;
        }

        private ToggleType _type;
        protected virtual ToggleType ToggleType
        {
            get => _type;
            set => _type = value; }

        protected BaseToggleSection(string enabledPropertyName, ToggleType toggle = ToggleType.Tick)
        {
            _enabledPropertyName = enabledPropertyName;
            _type = toggle;
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