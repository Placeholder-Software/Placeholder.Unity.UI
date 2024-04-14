using System;
using System.Collections.Generic;
using UnityEditor;

namespace Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// Throw an exception when this component is drawn
    /// </summary>
    public class ExceptionThrow
        : IComponent
    {
        public bool IsVisible => true;
        public bool RequiresConstantRepaint => false;

        public void OnEnable(SerializedObject target)
        {
        }

        public void OnDisable()
        {
        }

        public void Draw()
        {
            throw new Exception("This is a test");
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            yield break;
        }
    }
}
