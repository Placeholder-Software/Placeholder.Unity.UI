using JetBrains.Annotations;
using Placeholder.Editor.UI.Editor.Components;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor
{
    public abstract class BasePlaceholderEditor
        : UnityEditor.Editor
    {
        private readonly IComponent _root;
        private bool _defaultExpanded;

        protected BasePlaceholderEditor([NotNull] params IComponent[] components)
        {
            var root = new ComponentList();
            root.AddRange(components);
            _root = root;
        }

        protected BasePlaceholderEditor(IComponent root)
        {
            _root = root;
            _root.Editor = this;
        }

        public override bool RequiresConstantRepaint()
        {
            return _root.RequiresConstantRepaint;
        }

        [UsedImplicitly]
        protected virtual void OnEnable()
        {
            _root.Editor = this;
            _root.OnEnable(serializedObject);
        }

        [UsedImplicitly]
        protected virtual void OnDisable()
        {
            _root.OnDisable();
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