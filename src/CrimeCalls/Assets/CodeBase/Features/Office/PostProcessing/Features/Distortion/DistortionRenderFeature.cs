using UnityEngine;

namespace CodeBase.Features.Office.PostProcessing.Features.Distortion
{
    public class DistortionRenderFeature : CustomFullscreenRendererFeature
    {
        public Texture2D DistortionTexture;
        
        protected override CustomFullscreenRenderPass CreateCustomPass()
        {
            var pass = new DistortionPass(Shader);
            pass.Material.SetTexture("_DistortionTexture", DistortionTexture);
            return pass;
        }
    }
}