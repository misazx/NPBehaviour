using UnityEngine;

using UnityEngine.Rendering;



[CreateAssetMenu]

public class SRenderPipelineAsset : RenderPipelineAsset

{

    protected override RenderPipeline CreatePipeline()

    {

        return new SRenderPipeline();

    }

}