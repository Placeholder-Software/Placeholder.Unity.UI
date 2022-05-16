using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Plugins.PlaceholderSoftware.Placeholder.Editor.UI.Editor.Components.Addons.GenericMenu
{
    /// <summary>
    /// Provides a generic menu to be drawn in another component
    /// </summary>
    public interface IGenericMenu
    {
        [CanBeNull] UnityEditor.GenericMenu TryGetMenu(SerializedObject target, IComponent parent);
    }
}
