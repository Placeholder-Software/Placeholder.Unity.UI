using System.Collections.Generic;
using Assets.Editor.Placeholder.Editor.UI.Components.Addons.GenericMenu;
using Assets.Editor.Placeholder.Editor.UI.Components.Addons.Validation;
using Assets.Editor.Placeholder.Editor.UI.Style;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Sections
{
    /// <summary>
    /// Base class for sections in a UI which can be enabled/disabled/expanded/collapsed
    /// </summary>
    public abstract class BaseSection
        : IComponent
    {
        public virtual bool Expanded { get; set; }

        protected abstract bool Enabled { get; set; }

        [NotNull] protected abstract GUIContent Title { get; }

        protected SerializedObject Target { get; private set; }

        public virtual bool IsVisible => true;

        [CanBeNull] public IGenericMenu HeaderMenu { get; set; }
        [CanBeNull] public string HeaderUrl { get; set; }
        [CanBeNull] public IValidation Validation { get; set; } = new AttributeValidation();

        public virtual void OnEnable(SerializedObject target)
        {
            Target = target;
        }

        public void Draw()
        {
            using (new EditorGUILayout.VerticalScope(Expanded ? Styles.ContentOutline : GUIStyle.none))
            {
                Expanded = DrawHeader(
                    Title,
                    HeaderMenu?.TryGetMenu(Target),
                    HeaderUrl,
                    Validation?.TryGetErrorMessage(Target, this)
                );

                if (Expanded)
                    using (new EditorGUI.DisabledScope(!Enabled))
                    using (new EditorGUILayout.VerticalScope(Styles.LeftPadding))
                        DrawContent();
            }
        }

        public abstract IEnumerable<SerializedProperty> GetChildProperties();

        /// <summary>
        /// Draw the header for this section
        /// </summary>
        /// <param name="title"></param>
        /// <param name="menu"></param>
        /// <param name="helpUrl"></param>
        /// <param name="errorMessage"></param>
        /// <returns>Return whether the rest of the section content should be drawn</returns>
        protected abstract bool DrawHeader([NotNull] GUIContent title, [CanBeNull] GenericMenu menu, [CanBeNull] string helpUrl, [CanBeNull] string errorMessage);

        protected abstract void DrawContent();
    }
}