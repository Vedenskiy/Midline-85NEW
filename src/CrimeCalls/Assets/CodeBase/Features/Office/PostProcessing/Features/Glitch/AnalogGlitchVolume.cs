using System;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.Glitch
{
    [Serializable]
    [VolumeComponentMenu("Midline/Analog Glitch")]
    public class AnalogGlitchVolume : VolumeComponent
    {
        public ClampedFloatParameter ScanLineJitter = new(0, 0, 1);
        public ClampedFloatParameter VerticalJump = new(0, 0, 1);
        public ClampedFloatParameter HorizontalShake = new(0, 0, 1);
        public ClampedFloatParameter ColorDrift = new(0, 0, 1);
    }
}
