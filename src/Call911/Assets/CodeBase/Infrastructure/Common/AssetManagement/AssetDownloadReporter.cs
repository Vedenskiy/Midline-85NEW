using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Common.AssetManagement
{
    public class AssetDownloadReporter
    {
        public const float UpdateThreshold = 0.01f;
        
        public float Progress { get; private set; }

        public event Action Updated;

        public void Report(float newValue)
        {
            if (Mathf.Abs(Progress - newValue) < UpdateThreshold)
                return;

            Progress = newValue;
            Updated?.Invoke();
        }

        public void Reset()
        {
            Progress = 0;
        }
    }
}