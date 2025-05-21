using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.Distortion
{
    public class DistortionPass : CustomFullscreenRenderPass
    {
        public DistortionPass(Shader shader) : base(CoreUtils.CreateEngineMaterial(shader)) { }

        protected override void UpdateShaderParameters()
        {
            
        }
    }
}