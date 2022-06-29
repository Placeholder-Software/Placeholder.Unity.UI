using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components;
using JetBrains.Annotations;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor
{
    public class BasePlaceholderScriptedImporterEditor
        : ScriptedImporterEditor
    {
        private readonly IComponent _root;

        public BasePlaceholderScriptedImporterEditor([NotNull] params IComponent[] components)
        {
            var root = new ComponentList();
            root.AddRange(components);
            _root = root;
        }

        public BasePlaceholderScriptedImporterEditor(IComponent root)
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
        public override void OnEnable()
        {
            _root.OnEnable(serializedObject);

            base.OnEnable();
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
                serializedObject.Update();
                GUILayout.Space(4);
                _root.Draw();
                serializedObject.ApplyModifiedProperties();

                ApplyRevertGUI();
            }
        }
    }
}