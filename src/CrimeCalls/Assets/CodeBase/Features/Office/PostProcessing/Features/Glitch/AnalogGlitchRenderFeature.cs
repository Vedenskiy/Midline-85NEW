namespace CodeBase.Features.Office.PostProcessing.Features.Glitch
{
    public class AnalogGlitchRenderFeature : CustomFullscreenRendererFeature
    {
        protected override CustomFullscreenRenderPass CreateCustomPass() => 
            new AnalogGlitchPass(Shader);
    }
}