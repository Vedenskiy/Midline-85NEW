namespace CodeBase.Features.Office.PostProcessing.Features.BlinkEyes
{
    public class BlinkEyesRenderFeature : CustomFullscreenRendererFeature
    {
        protected override CustomFullscreenRenderPass CreateCustomPass() => 
            new BlinkEyesPass(Shader);
    }
}