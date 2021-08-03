using System;
using System.Collections.Generic;
using UnityEditor;

namespace Assets.Editor.Placeholder.Editor.UI.Components
{
    public class ExceptionThrow
        : IComponent
    {
        public bool IsVisible => true;

        public void OnEnable(SerializedObject target)
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
