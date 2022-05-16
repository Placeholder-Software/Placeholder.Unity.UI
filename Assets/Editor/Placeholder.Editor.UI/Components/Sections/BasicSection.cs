using System;
using Assets.Editor.Placeholder.Editor.UI.Helpers;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    /// <summary>
    /// A simple section with a title. Cannot be collapsed.
    /// </summary>
    public class BasicSection
        : BaseSection
    {
        protected override bool Enabled
        {
            get => true;
            set => throw new NotSupportedException();
        }

        public BasicSection([NotNull] GUIContent title, IComponent root)
            : base(title, root)
        {
        }

        public BasicSection([NotNull] GUIContent title, [NotNull] params IComponent[] components)
            : base(title, new ComponentList(null, null, components))
        {
        }

        protected override bool DrawHeader(GUIContent title, GenericMenu menu, string helpUrl, string errorMessage)
        {
            Header.Simple(title, new Header.HeaderOptions {
                DetailMenu = menu,
                HelpUrl = helpUrl,
                ErrorMessage = errorMessage
            });
            return true;
        }
    }
}