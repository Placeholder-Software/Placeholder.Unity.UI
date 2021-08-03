using Assets.Editor.Placeholder.Editor.UI;
using Assets.Editor.Placeholder.Editor.UI.Components;
using Assets.Editor.Placeholder.Editor.UI.Components.Addons.GenericMenu;
using Assets.Editor.Placeholder.Editor.UI.Components.Sections;
using Assets.Editor.Placeholder.Editor.UI.Style;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(TestScript))]
    [CanEditMultipleObjects]
    public class TestScriptEditor
        : BasePlaceholderEditor
    {
        public TestScriptEditor()
            : base(
                new ImageBanner("dissonance_logo"),
                new BasicComponentSection(
                    new GUIContent("Section 1"),
                    new SingleProperty("A"),
                    new SingleProperty("B"),
                    new SingleProperty("I"),
                    new SingleProperty("J")
                ) {
                    HeaderUrl = "https://example.com",
                },
                new FoldoutComponentSection(
                    new GUIContent("Section 2"),
                    new ComponentList(Color.cyan, new GUIContent("Foldout Section Title")) {
                        new SingleProperty("A"),
                        new SingleProperty("C"),
                        new SingleProperty("D")
                    },
                    new ComponentList(foldout: new GUIContent("Foldout Section Title 2")) {
                        new SingleProperty("A"),
                        new SingleProperty("C"),
                        new SingleProperty("D")
                    },
                    new ExceptionThrow(),
                    new ComponentList(Color.red) {
                        new ComponentList(Color.green) {
                            new SingleProperty("E"),
                            new SingleProperty("G")
                        },
                        new SingleProperty("H")
                    }
                ) {
                    HeaderMenu = new DemoGenericMenu(),
                    HeaderUrl = "https://example.com",
                },
                new FoldoutComponentSection(
                    new GUIContent("Section 3"),
                    new SingleProperty("A"),
                    new SingleProperty("F")
                ),
                new ToggleComponentSection(
                    new GUIContent("Section 4"),
                    "Section4Enabled",
                    ToggleType.Tick,
                    new SingleProperty("A"),
                    new InfoBoxComponent("The quick brown fox may jump over the lazy dog", MessageType.Warning),
                    new SingleProperty("E")
                ),
                new ToggleComponentSection(
                    new GUIContent("Muted Section"),
                    "MuteSection",
                    ToggleType.Mute,
                    new SingleProperty("E")
                ),
                new ToggleComponentSection(
                    new GUIContent("Bypass Section"),
                    "BypassSection",
                    ToggleType.Bypass,
                    new SingleProperty("E")
                ),
                new ToggleComponentSection(
                    new GUIContent("Solo Section"),
                    "SoloSection",
                    ToggleType.Solo,
                    new SingleProperty("E")
                ),
                new DefaultInspectorSection()
            )
        {
        }

        private class DemoGenericMenu
            : IGenericMenu
        {
            [NotNull]
            public GenericMenu TryGetMenu(SerializedObject target)
            {
                var g = new GenericMenu();
                g.AddItem(new GUIContent("Do"), false, () => { });
                g.AddDisabledItem(new GUIContent("The"));
                g.AddItem(new GUIContent("Thing"), false, () => { });
                return g;
            }
        }
    }
}
