using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Helpers;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// A list of components, drawn in order.
    /// </summary>
    public class ComponentList
        : IComponent, IEnumerable<IComponent>
    {
        private readonly Color? _strip;
        private readonly GUIContent _foldout;
        private readonly List<IComponent> _components = new List<IComponent>();

        private bool _foldoutState;

        private bool DoFoldout => _foldout != null;

        public virtual bool IsVisible => true;

        public ComponentList([NotNull] params IComponent[] components)
            : this(null, null, components)
        {
        }

        public ComponentList(Color? strip = null, [CanBeNull] GUIContent foldout = null, [NotNull] params IComponent[] components)
        {
            _strip = strip;
            _foldout = foldout;
            _components.AddRange(components);
        }

        public void OnEnable(SerializedObject target)
        {
            foreach (var component in _components)
                component.OnEnable(target);
        }

        public void Draw()
        {
            if (_strip.HasValue)
            {
                using (new EditorGUI.IndentLevelScope(1))
                using (new VerticalStripContext(_strip.Value))
                    DrawChildren();
            }
            else
                DrawChildren();
        }

        private void DrawChildren()
        {
            if (DoFoldout)
            {
                if (_strip.HasValue)
                    EditorGUI.indentLevel++;
                _foldoutState = EditorGUILayout.Foldout(_foldoutState, _foldout);
            }

            if (!DoFoldout || (DoFoldout && _foldoutState))
            {
                foreach (var component in _components)
                {
                    try
                    {
                        if (component.IsVisible)
                            component.Draw();
                    }
                    catch (Exception e)
                    {
                        using (new ColorScope(Color.red))
                            EditorGUILayout.LabelField($"Exception: {e.Message}");
                    }
                }
            }

            if (DoFoldout)
            {
                if (_strip.HasValue)
                    EditorGUI.indentLevel--;
            }
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            foreach (var component in _components)
                foreach (var property in component.GetChildProperties())
                    yield return property;
        }

        public void AddRange([NotNull] IReadOnlyList<IComponent> component)
        {
            _components.AddRange(component);
        }

        public void Add([NotNull] IComponent component)
        {
            _components.Add(component);
        }

        public IEnumerator<IComponent> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_components).GetEnumerator();
        }
    }
}
