namespace CodeBase.Features.Office.PostProcessing.Features.Distortion
{
    public class DistortionRenderFeature : CustomFullscreenRendererFeature
    {
        protected override CustomFullscreenRenderPass CreateCustomPass() => 
            new DistortionPass(Shader);
    }
}