using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.BlinkEyes
{
    public class BlinkEyesPass : CustomFullscreenRenderPass
    {
        private static readonly int  ProgressId = Shader.PropertyToID("_Progress");
        
        public BlinkEyesPass(Shader shader) : base(CoreUtils.CreateEngineMaterial(shader)) { }
        
        protected override void UpdateShaderParameters()
        {
            var volume = VolumeManager.instance.stack.GetComponent<BlinkEyesVolume>();
            Material.SetFloat(ProgressId, volume.Progress.value);
        }
    }
}