#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Placeholder.Editor.UI.Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// A list of components, drawn in order.
    /// </summary>
    public class ComponentList
        : IComponent, IEnumerable<IComponent>
    {
        private readonly Color? _strip;
        private readonly GUIContent? _foldout;
        private readonly List<IComponent> _components = new List<IComponent>();

        private bool _foldoutState;

        private bool DoFoldout => _foldout != null;

        public virtual bool IsVisible => true;

        public bool RequiresConstantRepaint => _components.Any(a => a.RequiresConstantRepaint);

        private BasePlaceholderEditor? _editor;
        public BasePlaceholderEditor? Editor
        {
            get => _editor;
            set
            {
                _editor = value;

                foreach (var component in _components)
                    component.Editor = value;
            }
        }

        public ComponentList(params IComponent[] components)
            : this(null, null, components)
        {
        }

        public ComponentList(Color? strip = null, params IComponent[] components)
            : this(strip, null, components)
        {
        }

        public ComponentList(Color? strip = null, GUIContent? foldout = null, params IComponent[] components)
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

        public void OnDisable()
        {
            foreach (var component in _components)
                component.OnDisable();
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
                        Debug.LogError(e);
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

        public void AddRange(IReadOnlyList<IComponent> components)
        {
            _components.AddRange(components);

            foreach (var component in components)
                component.Editor = Editor;
        }

        public void Add(IComponent component)
        {
            _components.Add(component);

            component.Editor = Editor;
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
