#nullable enable

using System;
using Placeholder.Editor.UI.Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components.Sections
{
    public class FoldoutSection
        : BaseSection
    {
        private readonly string? _expandedPropertyName;
        private bool _expandedField;
        private SerializedProperty? _expandedProperty;

        public override bool Expanded
        {
            get => _expandedProperty?.boolValue ?? _expandedField;
            set
            {
                if (_expandedProperty == null)
                    _expandedField = value;
                else
                    _expandedProperty.boolValue = value;
            }
        }

        protected override bool Enabled
        {
            get => true;
            set => throw new NotSupportedException();
        }

        public FoldoutSection(GUIContent title, IComponent component, string? expandedPropertyName = null)
            : base(title, component)
        {
            _expandedPropertyName = expandedPropertyName;
        }

        public override void OnEnable(SerializedObject target)
        {
            base.OnEnable(target);

            _expandedProperty = target.FindProperty(_expandedPropertyName);
        }

        protected override bool DrawHeader(GUIContent title, GenericMenu menu, string helpUrl, string errorMessage)
        {
            return Header.Fold(title, Expanded, new Header.HeaderOptions {
                DetailMenu = menu,
                HelpUrl = helpUrl,
                ErrorMessage = errorMessage
            });
        }
    }
}