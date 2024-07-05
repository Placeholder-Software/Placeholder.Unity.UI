using JetBrains.Annotations;
using Placeholder.Editor.UI.Editor.Style;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Helpers
{
    /// <summary>
    /// Helper methods to draw various types of header. Rather than using these methods directly you should probably use Components/Sections.
    /// </summary>
    public static class Header
    {
        private static void CoreHeader(GUIContent title, out Rect rect, HeaderOptions options)
        {
            const float barHeight = 22;
            const int iconSize = 18;
            const int errorIconSize = 14;
            const int errorIconMarginX = 3;
            const int errorIconMarginY = 3;

            // Draw background rect
            rect = GUILayoutUtility.GetRect(16f, barHeight, Styles.Header);
            GUI.Box(rect, title, Styles.Header);

            // Overdraw progress bar
            if (options.Progress > 0)
            {
                var t = Mathf.Clamp01(options.Progress);
                var left = rect;
                left.width *= t;

                var savedColor = GUI.backgroundColor;
                GUI.backgroundColor = options.ProgressColor;
                try
                {
                    GUI.Box(left, title, Styles.Header);
                }
                finally
                {
                    GUI.backgroundColor = savedColor;
                }
            }

            var hasError = options.ErrorMessage != null;

            // Draw the error icon in the left gutter. If there is no error still draw it but with zero size.
            var icon = EditorGUIUtility.IconContent("CollabConflict Icon").image;
            var errorIconRect = new Rect(rect.x - errorIconSize - errorIconMarginX, rect.y + errorIconMarginY, errorIconSize, errorIconSize);
            if (!hasError)
                errorIconRect.size = Vector2.zero;
            GUI.DrawTexture(errorIconRect, icon);
            GUI.Box(errorIconRect, new GUIContent("", options.ErrorMessage));

            // Calculate the rectangles for the two icons of the right hand side
            const float iconMarginY = (barHeight - iconSize) / 4;
            var detailRect = new Rect(rect.x + rect.width - iconSize - 5f, rect.y + iconMarginY, iconSize, iconSize);
            var helpRect = detailRect;

            // Display the icon for a generic menu dropdown on the right
            if (options.DetailMenu != null)
            {
                // Bump the other icons over to the left
                helpRect.x -= detailRect.width + 2;

                GUI.DrawTexture(detailRect, Styles.paneOptionsIcon);

                // Check if the user clicked the menu icon (expand rect to the full height of the header)
                var e = Event.current;
                var detailClick = new Rect(detailRect.xMin, rect.yMin, detailRect.width, rect.height);
                if (e.type == EventType.MouseDown && detailClick.Contains(e.mousePosition))
                {
                    options.DetailMenu.ShowAsContext();
                    e.Use();
                }
            }

            // Display the help icon
            if (options.HelpUrl != null)
            {
                GUI.DrawTexture(helpRect, Styles.helpIcon);

                // Check if the user clicked the help icon (expand rect to the full height of the header)
                var e = Event.current;
                var helpClick = new Rect(helpRect.xMin, rect.yMin, helpRect.width, rect.height);
                if (e.type == EventType.MouseDown && helpClick.Contains(e.mousePosition))
                {
                    Application.OpenURL(options.HelpUrl);
                    e.Use();
                }
            }
        }

        public struct HeaderOptions
        {
            [CanBeNull] public GenericMenu DetailMenu;
            [CanBeNull] public string HelpUrl;
            [CanBeNull] public string ErrorMessage;

            public float Progress;
            public Color ProgressColor;
        }

        /// <summary>
        /// Display a header with a title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="options"></param>
        public static void Simple(GUIContent title, HeaderOptions options = default)
        {
            CoreHeader(title, out _, options);
        }

        /// <summary>
        /// Display a header bar with a checkbox
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ticked"></param>
        /// <param name="expanded"></param>
        /// <param name="type"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static void Toggle(GUIContent title, ref bool ticked, ref bool expanded, ToggleType type = ToggleType.Tick, HeaderOptions options = default)
        {
            CoreHeader(title, out var rect, options);

            var style = type.ToStyle();
            var toggleRect = new Rect(rect.x + 3, rect.y + 3, style.fixedWidth, style.fixedHeight);
            var e = Event.current;

            if (e.type == EventType.Repaint)
                type.ToStyle().Draw(toggleRect, false, false, ticked, false);

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                if (toggleRect.Contains(e.mousePosition))
                {
                    ticked = !ticked;

                    if (ticked)
                        expanded = true;
                }
                else
                    expanded = !expanded;

                e.Use();
            }
        }

        /// <summary>
        /// Display a header bar with a fold arrow
        /// </summary>
        /// <param name="title"></param>
        /// <param name="folded"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool Fold(GUIContent title, bool folded, HeaderOptions options = default)
        {
            CoreHeader(title, out var rect, options);

            var foldoutRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            var e = Event.current;

            if (e.type == EventType.Repaint)
                Styles.HeaderFoldout.Draw(foldoutRect, false, false, folded, false);

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                folded = !folded;
                e.Use();
            }

            return folded;
        }
    }
}