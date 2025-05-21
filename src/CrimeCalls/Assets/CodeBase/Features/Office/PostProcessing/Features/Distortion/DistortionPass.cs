using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.Distortion
{
    public class DistortionPass : CustomFullscreenRenderPass
    {
        private static readonly int StrengthId = Shader.PropertyToID("_Strength");
        
        public DistortionPass(Shader shader) : base(CoreUtils.CreateEngineMaterial(shader)) { }

        protected override void UpdateShaderParameters()
        {
            var volume = VolumeManager.instance.stack.GetComponent<DistortionVolume>();
            var strength = new Vector2(volume.StrengthHorizontal.value, volume.StrengthVertical.value);
            Material.SetVector(StrengthId, strength);
        }
    }
}