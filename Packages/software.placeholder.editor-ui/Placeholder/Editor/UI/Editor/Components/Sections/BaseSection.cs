#nullable enable

using System.Collections.Generic;
using JetBrains.Annotations;
using Placeholder.Editor.UI.Editor.Components.Addons.GenericMenu;
using Placeholder.Editor.UI.Editor.Components.Addons.Validation;
using Placeholder.Editor.UI.Editor.Style;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components.Sections
{
    /// <summary>
    /// Base class for sections in a UI which can be enabled/disabled/expanded/collapsed
    /// </summary>
    public abstract class BaseSection
        : IComponent
    {
        private readonly IComponent _root;

        /// <summary>
        /// Indicates if this section is expanded (i.e. child components should be drawn)
        /// </summary>
        public virtual bool Expanded { get; set; }

        /// <summary>
        /// Indicates if this section is Enabled (i.e. if child components should be interactible)
        /// </summary>
        protected abstract bool Enabled { get; set; }

        /// <summary>
        /// Get the title of this section
        /// </summary>
        [NotNull] public GUIContent Title { get; set; }

        /// <summary>
        /// Get the object which this section is editing
        /// </summary>
        protected SerializedObject? Target { get; private set; }

        private BasePlaceholderEditor? _editor;
        public BasePlaceholderEditor? Editor
        {
            get => _editor;
            set
            {
                _editor = value;
                _root.Editor = value;
            }
        }

        public virtual bool IsVisible => true;

        public virtual bool RequiresConstantRepaint => _root.RequiresConstantRepaint;

        public IGenericMenu? HeaderMenu { get; set; }
        public string? HeaderUrl { get; set; }
        public IValidation? Validation { get; set; } = new AttributeValidation();

        protected BaseSection(GUIContent title, IComponent root)
        {
            Title = title;
            _root = root;
        }

        public virtual void OnEnable(SerializedObject target)
        {
            Target = target;
            _root.OnEnable(target);
        }

        public virtual void OnDisable()
        {
            _root.OnDisable();
        }

        public void Draw()
        {
            if (Target == null)
                return;

            using (new EditorGUILayout.VerticalScope(Expanded ? Styles.ContentOutline : GUIStyle.none))
            {
                Expanded = DrawHeader(
                    Title,
                    HeaderMenu?.TryGetMenu(Target, this),
                    HeaderUrl,
                    Validation?.TryGetErrorMessage(Target, this)
                );

                if (Expanded)
                    using (new EditorGUI.DisabledScope(!Enabled))
                    using (new EditorGUILayout.VerticalScope(Styles.LeftPadding))
                        DrawContent();
            }
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            return _root.GetChildProperties();
        }

        /// <summary>
        /// Draw the header for this section
        /// </summary>
        /// <param name="title"></param>
        /// <param name="menu"></param>
        /// <param name="helpUrl"></param>
        /// <param name="errorMessage"></param>
        /// <returns>Return whether the rest of the section content should be drawn</returns>
        protected abstract bool DrawHeader(GUIContent title, GenericMenu? menu, string? helpUrl, string? errorMessage);

        protected virtual void DrawContent()
        {
            _root.Draw();
        }
    }
}