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

            //相机裁剪
            camera.TryGetCullingParameters(out var parameters);
            CullingResults results = context.Cull(ref parameters);

            DrawingSettings ds = new DrawingSettings();
            //指定使用设定的LightMode的Pass
            ds.SetShaderPassName(0, new ShaderTagId("SForward"));
            //排序设置
            ds.sortingSettings = new SortingSettings(camera) { criteria = SortingCriteria.CommonOpaque };
            //过滤设置
            FilteringSettings fs = new FilteringSettings(RenderQueueRange.opaque, -1);

            context.DrawRenderers(results, ref ds, ref fs);
        }

        context.Submit();
    }

}