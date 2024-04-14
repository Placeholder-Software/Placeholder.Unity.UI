using JetBrains.Annotations;
using Placeholder.Editor.UI.Editor.Components;
using UnityEditor;
using UnityEngine;

namespace Placeholder.Editor.UI.Editor
{
    public abstract class BasePlaceholderShaderGUIEditor
        : ShaderGUI
    {
        //todo:
        // - OnEnable for root component
        // - OnDisable for root component

        private readonly IComponent _root;

        private bool _enabled;
        private SerializedObject _serializedObject;

        #region constructors
        protected BasePlaceholderShaderGUIEditor([NotNull] params IComponent[] components)
        {
            var root = new ComponentList();
            root.AddRange(components);
            _root = root;
        }

        protected BasePlaceholderShaderGUIEditor(IComponent root)
        {
            _root = root;
        }
        #endregion

        /// <summary>
        /// If this returns `true` the default inspector will be drawn
        /// </summary>
        /// <returns></returns>
        protected virtual bool UseDefaultInspector()
        {
            return false;
        }

        public override void OnClosed(Material material)
        {
            _enabled = false;
            _root.OnDisable();
            Debug.LogWarning("OnDisable");

            base.OnClosed(material);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            if (!_enabled)
            {
                _serializedObject = materialEditor.serializedObject;
                _root.OnEnable(_serializedObject);
                _enabled = true;
            }

            EditorGUILayout.LabelField("TODO: CUSTOM INSPECTOR");
            var exampleProperty = FindProperty("_ExampleName", properties);
            materialEditor.ShaderProperty(exampleProperty, exampleProperty.displayName);

            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel(exampleProperty.displayName);
                EditorGUILayout.FloatField(exampleProperty.floatValue);
            }

            if (UseDefaultInspector())
            {
                base.OnGUI(materialEditor, properties);
            }
            else
            {
                GUILayout.Space(4);
                _root.Draw();
            }

            //var prop = MaterialEditor.GetMaterialProperty(materialEditor.targets, "_ExampleName");
            //materialEditor.FloatProperty(prop, "Label");
            //Debug.Log(materialEditor.serializedObject.FindProperty("_ExampleName"));

            //var material = (Material)materialEditor.target;
            //Debug.Log(material.HasProperty("_ExampleName"));



            //var iter = _serializedObject.GetIterator();
            //while (iter.Next(true))
            //{
            //    Debug.LogWarning(iter.displayName);
            //}

            //// get the current keywords from the material
            //var targetMat = (Material)materialEditor.target;
            //var keyWords = targetMat.shaderKeywords;

            //// see if redify is set, then show a checkbox
            //var redify = keyWords.Contains("REDIFY_ON");
            //EditorGUI.BeginChangeCheck();
            //redify = EditorGUILayout.Toggle("Redify material", redify);
            //if (EditorGUI.EndChangeCheck())
            //{
            //    // if the checkbox is changed, reset the shader keywords
            //    var keywords = new List<string> { redify ? "REDIFY_ON" : "REDIFY_OFF" };
            //    targetMat.shaderKeywords = keywords.ToArray();
            //    EditorUtility.SetDirty(targetMat);
            //}
        }

        public override void OnMaterialPreviewGUI(MaterialEditor materialEditor, Rect r, GUIStyle background)
        {
            base.OnMaterialPreviewGUI(materialEditor, r, background);
        }

        public override void OnMaterialPreviewSettingsGUI(MaterialEditor materialEditor)
        {
            base.OnMaterialPreviewSettingsGUI(materialEditor);
        }

        public override void ValidateMaterial(Material material)
        {
            base.ValidateMaterial(material);
        }

        public override void OnMaterialInteractivePreviewGUI(MaterialEditor materialEditor, Rect r, GUIStyle background)
        {
            base.OnMaterialInteractivePreviewGUI(materialEditor, r, background);
        }
    }
}
