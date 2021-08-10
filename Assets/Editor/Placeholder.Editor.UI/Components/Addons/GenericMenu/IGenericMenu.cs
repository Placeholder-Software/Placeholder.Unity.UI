using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Editor.Placeholder.Editor.UI.Components.Addons.GenericMenu
{
    /// <summary>
    /// Provides a generic menu to be drawn in another component
    /// </summary>
    public interface IGenericMenu
    {
        [CanBeNull] UnityEditor.GenericMenu TryGetMenu(SerializedObject target, IComponent parent);
    }
}
