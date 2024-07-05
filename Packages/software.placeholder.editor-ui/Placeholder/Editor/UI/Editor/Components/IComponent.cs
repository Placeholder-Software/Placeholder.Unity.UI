#nullable enable

using System.Collections.Generic;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Components
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
        /// Get if this component should be re-rendered every frame
        /// </summary>
        bool RequiresConstantRepaint { get; }

        /// <summary>
        /// Called when this component is enabled
        /// </summary>
        /// <param name="target">The object which is being edited</param>
        void OnEnable(SerializedObject target);

        /// <summary>
        /// Get or set the root editor which contains this component
        /// </summary>
        BasePlaceholderEditor? Editor { get; set; }

        /// <summary>
        /// Called when this component is disabled
        /// </summary>
        void OnDisable();

        /// <summary>
        /// Render this component
        /// </summary>
        void Draw();

        /// <summary>
        /// Get all properties which are contained within this component
        /// </summary>
        /// <returns></returns>
        IEnumerable<SerializedProperty> GetChildProperties();
    }
}
