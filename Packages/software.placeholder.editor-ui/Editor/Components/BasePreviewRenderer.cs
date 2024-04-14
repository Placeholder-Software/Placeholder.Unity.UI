using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor.Components
{
    /// <summary>
    /// Renders a scene into a GUI area
    /// </summary>
    public abstract class BasePreviewRenderer
        : IComponent
    {
        private PreviewRenderUtility _renderUtility;

        public bool IsVisible => true;
        public abstract bool RequiresConstantRepaint { get; }

        /// <summary>
        /// The aspect ratio which the display will have. Width and height will be adapted to always have this ratio.
        /// </summary>
        protected abstract float AspectRatio { get; }

        /// <summary>
        /// Background colour to clear to (ignored if `ClearWithSkybox = true`)
        /// </summary>
        protected abstract Vector4 BackgroundColor { get; }

        /// <summary>
        /// If true, the background will be cleared to the skybox instead
        /// </summary>
        protected abstract bool ClearWithSkybox { get; }

        public virtual void OnEnable(SerializedObject target)
        {
            _renderUtility ??= new PreviewRenderUtility(false);
            SetupPreviewScene(_renderUtility);
        }

        /// <summary>
        /// Called just once to create the initial scene.<br />
        /// Use `renderUtility.AddSingleGO(_targetObject);` to move a GameObject into the preview scene.
        /// </summary>
        /// <param name="renderUtility"></param>
        protected abstract void SetupPreviewScene(PreviewRenderUtility renderUtility);

        public virtual void OnDisable()
        {
            _renderUtility?.Cleanup();
            _renderUtility = null;
        }

        public void Draw()
        {
            // Calculate the area to draw the preview in
            var width = EditorGUIUtility.currentViewWidth - 50;
            var height = width / AspectRatio;
            var area = EditorGUILayout.GetControlRect(false, height);
            area.yMax -= EditorGUIUtility.singleLineHeight / 2;
            area.width = width;

            // Draw preview
            if (area is { width: > 0, height: > 0 })
            {
                // Configure camera
                var camera = _renderUtility.camera;
                ConfigureCamera(camera, new Vector2(area.width, area.height));

                // Do the render into an image
                _renderUtility.BeginPreview(area, GUIStyle.none);
                {
                    if (ClearWithSkybox)
                        GL.ClearWithSkybox(true, camera);
                    else
                        GL.Clear(true, true, BackgroundColor);

                    PreRender(_renderUtility);
                    _renderUtility.Render(updatefov: false);
                    PostRender(_renderUtility);
                }
                var image = _renderUtility.EndPreview();

                // Draw the image to the UI
                GL.sRGBWrite = QualitySettings.activeColorSpace == ColorSpace.Linear;
                GUI.DrawTexture(area, image, ScaleMode.StretchToFill, true);
                GL.sRGBWrite = false;
            }
        }

        /// <summary>
        /// Called just before the scene is rendered
        /// </summary>
        /// <param name="renderUtility"></param>
        protected virtual void PreRender(PreviewRenderUtility renderUtility)
        {
        }

        /// <summary>
        /// Called just after the scene is renderer
        /// </summary>
        /// <param name="renderUtility"></param>
        protected virtual void PostRender(PreviewRenderUtility renderUtility)
        {
        }

        /// <summary>
        /// Called every time rendering is about to happen
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="viewport"></param>
        protected virtual void ConfigureCamera(Camera camera, Vector2 viewport)
        {
            var aspect = viewport.x / viewport.y;
            camera.aspect = aspect;
        }

        public IEnumerable<SerializedProperty> GetChildProperties()
        {
            return Array.Empty<SerializedProperty>();
        }
    }
}
