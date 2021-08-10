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
                        new Property("A"),
                        new Property("B"),
                        new Property("I"),
                        new Property("J")
                    )
                ) {
                    HeaderUrl = "https://example.com",
                },
                new FoldoutSection(
                    new GUIContent("Section 2"),
                    new ComponentList {
                        new ComponentList(Color.cyan, new GUIContent("Foldout Section Title")) {
                            new Property("A"),
                            new Property("C"),
                            new Property("D")
                        },
                        new ComponentList(foldout: new GUIContent("Foldout Section Title 2")) {
                            new Property("A"),
                            new Property("C"),
                            new Property("D")
                        },
                        new ExceptionThrow(),
                        new ComponentList(Color.red) {
                            new ComponentList(Color.green) {
                                new Property("E1"),
                                new Property("G")
                            },
                            new Property("H")
                        }
                    }
                ) {
                    HeaderMenu = new DemoGenericMenu(),
                    HeaderUrl = "https://example.com",
                },
                new FoldoutSection(
                    new GUIContent("Section 3"),
                    new ComponentList {
                        new Property("A"),
                        new Property("F"),
                        new Property("K"),
                    }
                ),
                new ToggleSection(
                    new GUIContent("Section 4"),
                    "Section4Enabled",
                    new ComponentList {
                        new Property("A"),
                        new InfoBoxComponent("The quick brown fox may jump over the lazy dog", MessageType.Warning),
                        new Property("B"),
                    }
                ) {
                    HeaderMenu = new Section4CopyPaste(),
                },
                new ToggleSection(
                    new GUIContent("Muted Section"),
                    "MuteSection",
                    new Property("E1")
                ) {
                    ToggleType = ToggleType.Mute,
                },
                new ToggleSection(
                    new GUIContent("Bypass Section"),
                    "BypassSection",
                    new Property("E2")
                ) {
                    ToggleType = ToggleType.Bypass,
                },
                new ToggleSection(
                    new GUIContent("Solo Section"),
                    "SoloSection",
                    new Property("E3")
                ) {
                    ToggleType = ToggleType.Solo,
                },
                new DefaultInspectorSection()
            )
        {
        }

        private class Section4CopyPaste
            : BaseCopyPasteMenu<Section4CopyPaste.CopyPasteData>
        {
            internal struct CopyPasteData
            {
                public float A;
                public float B;
            }

            [NotNull] public override GenericMenu TryGetMenu(SerializedObject target, IComponent parent)
            {
                var g = base.TryGetMenu(target, parent);
                g.AddSeparator("");
                g.AddDisabledItem(new GUIContent("Section 4"));
                return g;
            }

            protected override CopyPasteData Copy(SerializedObject target, IComponent parent)
            {
                return new CopyPasteData {
                    A = target.FindProperty("A").floatValue,
                    B = target.FindProperty("B").floatValue
                };
            }

            protected override void Paste(SerializedObject target, IComponent parent, CopyPasteData data)
            {
                target.FindProperty("A").floatValue = data.A;
                target.FindProperty("B").floatValue = data.B;
                target.ApplyModifiedProperties();
            }
        }

        private class DemoGenericMenu
            : IGenericMenu
        {
            [NotNull]
            public GenericMenu TryGetMenu(SerializedObject target, IComponent parent)
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
