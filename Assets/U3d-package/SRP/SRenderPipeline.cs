using UnityEngine;

using UnityEngine.Rendering;



public class SRenderPipeline : RenderPipeline
{
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            Camera camera = cameras[i];
            context.SetupCameraProperties(camera);
            context.DrawSkybox(camera);

            //����ü�
            camera.TryGetCullingParameters(out var parameters);
            CullingResults results = context.Cull(ref parameters);

            DrawingSettings ds = new DrawingSettings();
            //ָ��ʹ���趨��LightMode��Pass
            ds.SetShaderPassName(0, new ShaderTagId("SForward"));
            //��������
            ds.sortingSettings = new SortingSettings(camera) { criteria = SortingCriteria.CommonOpaque };
            //��������
            FilteringSettings fs = new FilteringSettings(RenderQueueRange.opaque, -1);

            context.DrawRenderers(results, ref ds, ref fs);
        }

        context.Submit();
    }

}