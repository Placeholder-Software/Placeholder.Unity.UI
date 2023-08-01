using Placeholder.Editor.UI.Editor.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class DemoPreviewRenderer
    : BasePreviewRenderer
{
    public override bool RequiresConstantRepaint => true;

    protected override float AspectRatio => 2.2f;
    protected override Vector4 BackgroundColor => new Color(0.15f, 0.15f, 0.15f, 1);
    protected override bool ClearWithSkybox => false;

    private Mesh _mesh;
    private Material _material;

    public override void OnEnable(SerializedObject target)
    {
        base.OnEnable(target);

        // Hacky way to grab a cube mesh
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _mesh = go.GetComponent<MeshFilter>().sharedMesh;
        Object.DestroyImmediate(go);

        // default material from whatever pipeline is appropriate
        _material = GraphicsSettings.defaultRenderPipeline?.defaultMaterial;
        if (_material == null)
            _material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
    }

    protected override void SetupPreviewScene(PreviewRenderUtility renderUtility)
    {
        var targetObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        renderUtility.AddSingleGO(targetObject);

        var camera = renderUtility.camera;
        camera.transform.position = new Vector3(0f, 0f, -10f);
        camera.nearClipPlane = 5f;
        camera.farClipPlane = 20f;
    }

    protected override void PreRender(PreviewRenderUtility renderUtility)
    {
        var mat = Matrix4x4.Translate(Vector3.one);
        renderUtility.DrawMesh(_mesh, mat, _material, 0);
    }
}

