using JetBrains.Annotations;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Components.Addons.Validation
{
    /// <summary>
    /// Generate error messages associated with the given UI component
    /// </summary>
    public interface IValidation
    {
        [CanBeNull] string TryGetErrorMessage([NotNull] SerializedObject target, [NotNull] IComponent parent);
    }
}
