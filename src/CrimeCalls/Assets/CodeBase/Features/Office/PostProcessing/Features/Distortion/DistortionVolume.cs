using System;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.Distortion
{
    [Serializable]
    [VolumeComponentMenu("Midline/Distortion")]
    public class DistortionVolume : VolumeComponent
    {
        public ClampedFloatParameter Strength = new(0, 0, 1);
    }
}