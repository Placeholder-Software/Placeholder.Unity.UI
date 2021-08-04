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
                new BasicSection(
                    new GUIContent("Section 1"),
                    new ComponentList(
                        new SingleProperty("A"),
                        new SingleProperty("B"),
                        new SingleProperty("I"),
                        new SingleProperty("J")
                    )
                ) {
                    HeaderUrl = "https://example.com"
                },
                new FoldoutSection(
                    new GUIContent("Section 2"),
                    new ComponentList {
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
                    }
                ) {
                    HeaderMenu = new DemoGenericMenu(),
                    HeaderUrl = "https://example.com",
                },
                new FoldoutSection(
                    new GUIContent("Section 3"),
                    new ComponentList {
                        new SingleProperty("A"),
                        new SingleProperty("F")
                    }
                ),
                new ToggleSection(
                    new GUIContent("Section 4"),
                    "Section4Enabled",
                    new ComponentList {
                        new SingleProperty("A"),
                        new InfoBoxComponent("The quick brown fox may jump over the lazy dog", MessageType.Warning),
                        new SingleProperty("E")
                    }
                ),
                new ToggleSection(
                    new GUIContent("Muted Section"),
                    "MuteSection",
                    new SingleProperty("E")
                ) {
                    ToggleType = ToggleType.Mute
                },
                new ToggleSection(
                    new GUIContent("Bypass Section"),
                    "BypassSection",
                    new SingleProperty("E")
                ) {
                    ToggleType = ToggleType.Bypass
                },
                new ToggleSection(
                    new GUIContent("Solo Section"),
                    "SoloSection",
                    new SingleProperty("E")
                ) {
                    ToggleType = ToggleType.Solo
                },
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
