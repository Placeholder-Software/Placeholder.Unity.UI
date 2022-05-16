using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components.Addons.GenericMenu
{
    public abstract class BaseCopyPasteMenu<T>
        : IGenericMenu
        where T : struct
    {
        [NotNull] public virtual UnityEditor.GenericMenu TryGetMenu(SerializedObject target, IComponent parent)
        {
            var g = new UnityEditor.GenericMenu();

            var ctx = new Context(this, target, parent);
            g.AddItem(new GUIContent("Copy"), false, ctx.DoCopy);

            if (Context.CanPaste())
                g.AddItem(new GUIContent("Paste"), false, ctx.DoPaste);
            else
                g.AddDisabledItem(new GUIContent("Paste"));

            return g;
        }

        protected abstract T Copy([NotNull] SerializedObject target, [NotNull] IComponent parent);

        protected abstract void Paste([NotNull] SerializedObject target, [NotNull] IComponent parent, T data);

        private class Context
        {
            private readonly BaseCopyPasteMenu<T> _menu;
            private readonly SerializedObject _target;
            private readonly IComponent _parent;

            public Context(BaseCopyPasteMenu<T> menu, SerializedObject target, IComponent parent)
            {
                _menu = menu;
                _target = target;
                _parent = parent;
            }

            public void DoPaste()
            {
                try
                {
                    var data = JsonUtility.FromJson<T>(EditorGUIUtility.systemCopyBuffer);
                    _menu.Paste(_target, _parent, data);
                }
                catch
                {
                    Debug.LogWarning("Paste operation failed to deserialise data");
                }
            }

            public void DoCopy()
            {
                var data = _menu.Copy(_target, _parent);
                EditorGUIUtility.systemCopyBuffer = JsonUtility.ToJson(data);
            }

            public static bool CanPaste()
            {
                try
                {
                    JsonUtility.FromJson<T>(EditorGUIUtility.systemCopyBuffer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
