using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.Glitch
{
    public class AnalogGlitchPass : CustomFullscreenRenderPass
    {
        private static readonly int ScanLineJitterId = Shader.PropertyToID("_ScanLineJitter");
        private static readonly int VerticalJumpId = Shader.PropertyToID("_VerticalJump");
        private static readonly int HorizontalShakeId = Shader.PropertyToID("_HorizontalShake");
        private static readonly int ColorDriftId = Shader.PropertyToID("_ColorDrift");

        private float _verticalJumpTime;

        public AnalogGlitchPass(Shader shader) : base(CoreUtils.CreateEngineMaterial(shader)) { }

        protected override string GetPassName() => 
            "AnalogGlitchPass";

        protected override void UpdateShaderParameters()
        {
            if (Material == null)
            {
                Debug.LogError($"{nameof(AnalogGlitchPass)}"); 
                return;
            }
            
            var volume = VolumeManager.instance.stack.GetComponent<AnalogGlitchVolume>();

            var scanLineJitter = volume.ScanLineJitter.value;
            var verticalJump = volume.VerticalJump.value;
            var horizontalShake = volume.HorizontalShake.value;
            var colorDrift = volume.ColorDrift.value;

            _verticalJumpTime += Time.deltaTime * verticalJump * 11.3f;

            var slThresh = Mathf.Clamp01(1.0f - scanLineJitter * 1.2f);
            var slDisp = 0.002f + Mathf.Pow(scanLineJitter, 3) * 0.05f;
            Material.SetVector(ScanLineJitterId, new Vector2(slDisp, slThresh));

            var vj = new Vector2(verticalJump, _verticalJumpTime);
            Material.SetVector(VerticalJumpId, vj);
            Material.SetFloat(HorizontalShakeId, horizontalShake * 0.2f);

            var cd = new Vector2(colorDrift * 0.04f, Time.time * 606.11f);
            Material.SetVector(ColorDriftId, cd);
        }
    }
}