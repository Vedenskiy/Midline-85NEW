using System;
using UnityEngine.Rendering;

namespace CodeBase.Features.Office.PostProcessing.Features.BlinkEyes
{
    [Serializable]
    [VolumeComponentMenu("Midline/Blink Eyes")]
    public class BlinkEyesVolume : VolumeComponent
    {
        public ClampedFloatParameter Progress = new(0, 0, 1);
    }
}