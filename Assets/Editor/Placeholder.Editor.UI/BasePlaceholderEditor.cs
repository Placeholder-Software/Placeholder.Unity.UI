using Assets.Editor.Placeholder.Editor.UI.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Editor.Placeholder.Editor.UI
{
    public class BasePlaceholderEditor
        : UnityEditor.Editor
    {
        private readonly IComponent _root;

        public BasePlaceholderEditor([NotNull] params IComponent[] components)
        {
            var root = new ComponentList();
            root.AddRange(components);
            _root = root;
        }

        public BasePlaceholderEditor(IComponent root)
        {
            _root = root;
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            _root.OnEnable(serializedObject);
        }

        [UsedImplicitly]
        public override void OnInspectorGUI()
        {
            GUILayout.Space(4);
            _root.Draw();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
