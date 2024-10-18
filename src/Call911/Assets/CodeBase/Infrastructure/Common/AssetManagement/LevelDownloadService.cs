using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CodeBase.Features.Calls.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace CodeBase.Infrastructure.Common.AssetManagement
{
    public class LevelDownloadService
    {
        public async UniTask LoadDialogue(string callName, CancellationToken token = default)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(callName);
            DebugLocations(locations);

            var downloadSize = await Addressables.GetDownloadSizeAsync(locations);
            Debug.Log($"download size: {SizeToMb(downloadSize)} Mb");

            var downloadHandle = Addressables.DownloadDependenciesAsync(locations);
            while (!downloadHandle.IsDone && downloadHandle.IsValid())
            {
                await UniTask.Delay(100, cancellationToken: token);
                Debug.Log($"Download percent: {downloadHandle.GetDownloadStatus().Percent}");
            }
            
            if (downloadHandle.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Error while downloading catalog dependencies");
            
            if (downloadHandle.IsValid())
                Addressables.Release(downloadHandle);
            
            Debug.Log($"Call {callName} loaded!");
        }
        
        private static float SizeToMb(long downloadSize) => downloadSize * 1f / 1048576;

        private void DebugLocations(IEnumerable<IResourceLocation> locations)
        {
            var builder = new StringBuilder();
            foreach (var location in locations) 
                builder.Append($"{location};");
            Debug.Log($"Locations: {builder}");
        }

    }
}