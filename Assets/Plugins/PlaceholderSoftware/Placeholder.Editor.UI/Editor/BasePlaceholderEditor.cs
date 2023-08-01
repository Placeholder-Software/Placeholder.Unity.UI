using JetBrains.Annotations;
using Placeholder.Editor.UI.Editor.Components;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor
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

        public override bool RequiresConstantRepaint()
        {
            return _root.RequiresConstantRepaint;
        }

        /// <summary>
        /// If this returns `true` the default inspector will be drawn
        /// </summary>
        /// <returns></returns>
        protected virtual bool UseDefaultInspector()
        {
            return false;
        }

        [UsedImplicitly]
        protected virtual void OnEnable()
        {
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
            if (UseDefaultInspector())
            {
                base.OnInspectorGUI();
            }
            else
            {
                GUILayout.Space(4);
                _root.Draw();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}