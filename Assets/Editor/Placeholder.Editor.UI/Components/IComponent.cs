using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;

namespace Assets.Editor.Placeholder.Editor.UI.Components
{
    /// <summary>
    /// A single component of a user interface. May contain other components.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Get if this component should be rendered
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        /// Called when this component is enabled
        /// </summary>
        /// <param name="target">The object which is being edited</param>
        void OnEnable([NotNull] SerializedObject target);

        /// <summary>
        /// Render this component
        /// </summary>
        void Draw();

        /// <summary>
        /// Get all properties which are contained within this component
        /// </summary>
        /// <returns></returns>
        [NotNull, ItemNotNull] IEnumerable<SerializedProperty> GetChildProperties();
    }
}
