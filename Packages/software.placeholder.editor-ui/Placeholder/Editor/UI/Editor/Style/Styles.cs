using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Style
{
    public static class Styles
    {
        public static readonly GUIStyle Header;
        public static readonly GUIStyle HeaderProgress;
        public static readonly GUIStyle HeaderTickToggle;
        public static readonly GUIStyle HeaderSoloToggle;
        public static readonly GUIStyle HeaderMuteToggle;
        public static readonly GUIStyle HeaderBypassToggle;
        public static readonly GUIStyle HeaderMixedToggle;
        public static readonly GUIStyle HeaderFoldout;
        
        public static readonly Texture2D playIcon;
        public static readonly Texture2D checkerIcon;
        public static readonly Texture2D paneOptionsIcon;
        public static readonly Texture2D helpIcon;

        public static readonly GUIStyle centeredMiniLabel;

        public static readonly GUIStyle LeftPadding;
        public static readonly GUIStyle ContentOutline;

        static Styles()
        {
            centeredMiniLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel) {
                alignment = TextAnchor.UpperCenter
            };

            Header = new GUIStyle("ShurikenModuleTitle") {
                font = new GUIStyle("Label").font,
                border = new RectOffset(15, 7, 4, 4),
                fixedHeight = 22,
                contentOffset = new Vector2(20f, -2f)
            };

            HeaderProgress = new GUIStyle("ShurikenModuleTitle")
            {
                font = new GUIStyle("Label").font,
                border = new RectOffset(4, 4, 2, 2),
                fixedHeight = 22,
            };

            static GUIStyle ToggleDimension(GUIStyle s)
            {
                s = new GUIStyle(s) {
                    fixedHeight = 14,
                    fixedWidth = 14
                };
                return s;
            }

            HeaderTickToggle = ToggleDimension("ShurikenToggle");
            HeaderSoloToggle = ToggleDimension("SoloToggle");
            HeaderMuteToggle = ToggleDimension("MuteToggle");
            HeaderBypassToggle = ToggleDimension("BypassToggle");
            HeaderMixedToggle = ToggleDimension("ShurikenToggleMixed");

            HeaderFoldout = "Foldout";

            playIcon = (Texture2D) EditorGUIUtility.LoadRequired("Builtin Skins/DarkSkin/Images/IN foldout act.png");
            checkerIcon = (Texture2D) EditorGUIUtility.LoadRequired("Icons/CheckerFloor.png");
            helpIcon = (Texture2D) EditorGUIUtility.IconContent("_Help").image;

            if (EditorGUIUtility.isProSkin)
                paneOptionsIcon = (Texture2D) EditorGUIUtility.LoadRequired("Builtin Skins/DarkSkin/Images/pane options.png");
            else
                paneOptionsIcon = (Texture2D) EditorGUIUtility.LoadRequired("Builtin Skins/LightSkin/Images/pane options.png");

            LeftPadding = new GUIStyle {
                padding = new RectOffset(15, 0, 0, 0)
            };

            ContentOutline = new GUIStyle(EditorStyles.helpBox) {
                padding = new RectOffset(0, 0, 0, 1),
                margin = new RectOffset(0, 0, 0, 3)
            };
        }
    }
}