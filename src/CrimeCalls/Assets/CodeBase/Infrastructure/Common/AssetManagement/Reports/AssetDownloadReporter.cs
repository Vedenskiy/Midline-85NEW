using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Common.AssetManagement.Reports
{
    public class AssetDownloadReporter
    {
        public const float UpdateThreshold = 0.01f;
        
        public float Progress { get; private set; }
        public long DownloadSize { get; set; }

        public event Action<AssetDownloadReporter> Updated;

        public void Report(float newValue)
        {
            if (Mathf.Abs(Progress - newValue) < UpdateThreshold)
                return;

            Progress = newValue;
            Updated?.Invoke(this);
        }

        public void Reset() => 
            Progress = 0;

        public float GetDownloadSizeMb() => 
            SizeToMb(DownloadSize);

        private static float SizeToMb(long downloadSize) => downloadSize * 1f / 1048576;

        public void UpdateTargetDownloadSize(long downloadSize) => 
            DownloadSize = downloadSize;
    }
}