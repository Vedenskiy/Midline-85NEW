using System;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.Distortion
{
    [Serializable]
    [VolumeComponentMenu("Midline/Distortion")]
    public class DistortionVolume : VolumeComponent
    {
        public ClampedFloatParameter StrengthVertical = new(0, 0, 1);
        public ClampedFloatParameter StrengthHorizontal = new(0, 0, 1);
    }
}