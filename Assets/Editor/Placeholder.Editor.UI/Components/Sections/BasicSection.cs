using System;
using Assets.Editor.Placeholder.Editor.UI.Helpers;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    /// <summary>
    /// A simple section with a title. Cannot be collapsed.
    /// </summary>
    public abstract class BasicSection
        : BaseSection
    {
        protected override bool Enabled
        {
            get => true;
            set => throw new NotSupportedException();
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