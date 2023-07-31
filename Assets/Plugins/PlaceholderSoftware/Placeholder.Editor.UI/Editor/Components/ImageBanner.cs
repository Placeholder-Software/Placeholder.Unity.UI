#nullable enable

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// Display a banner with an image
    /// </summary>
    public class ImageBanner
        : IComponent
    {
        private readonly string _resourcePath;
        private Texture2D? _image;

        public bool IsVisible => true;
        public bool RequiresConstantRepaint => false;

        public ImageBanner(string resourcePath)
        {
            _resourcePath = resourcePath;
        }

        public void OnEnable(SerializedObject target)
        {
            _image = Resources.Load<Texture2D>(_resourcePath);
        }

        public void Draw()
        {
            if (_image != null)
            {
                GUILayout.Space(2);
                GUILayout.Label(_image);
                GUILayout.Space(5);
            }
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            return Array.Empty<SerializedProperty>();
        }
    }
}