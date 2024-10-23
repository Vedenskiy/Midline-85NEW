using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CodeBase.Features.Calls.Infrastructure;
using Cysharp.Threading.Tasks;
using Nadsat.DialogueGraph.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Common.AssetManagement
{
    public class LevelDownloadService
    {
        private readonly AssetProvider _assets;
        private readonly DialogueGraphAdapter _adapter;

        private long _downloadSize;

        public LevelDownloadService(AssetProvider assets, DialogueGraphAdapter adapter)
        {
            _assets = assets;
            _adapter = adapter;
        }

        public async UniTask<bool> IsLoadedLevel(string levelName, CancellationToken token = default)
        {
            var downloadSize = await Addressables.GetDownloadSizeAsync(levelName);
            var paths = new List<string>();
            Caching.GetAllCachePaths(paths);
            foreach (var path in paths)
            {
                Debug.Log($"cached {path}");
            }
            Debug.Log($"{downloadSize}");
            return downloadSize <= 0;
        }

        public async UniTask<Dialogue> LoadDialogue(string callName, Action<float> onProgress = null, CancellationToken token = default)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(callName);
            DebugLocations(locations);
            
            var downloadSize = await Addressables.GetDownloadSizeAsync(locations);
            Debug.Log($"download size: {SizeToMb(downloadSize)} Mb");
            _downloadSize = downloadSize;
            
            await DownloadDependenciesAsync(locations, onProgress, token);
            Debug.Log($"Call {callName} loaded!");

            var dialogue = await LoadDialogue(locations);
            Debug.Log($"Completed dialogue loaded!");
            return dialogue;
        }

        public float GetDownloadSizeMb() =>
            SizeToMb(_downloadSize);

        private async UniTask DownloadDependenciesAsync(IList<IResourceLocation> locations, Action<float> onProgress = null, CancellationToken token = default)
        {
            var downloadHandle = Addressables.DownloadDependenciesAsync(locations);
            while (!downloadHandle.IsDone && downloadHandle.IsValid())
            {
                await UniTask.Delay(100, cancellationToken: token);
                onProgress?.Invoke(downloadHandle.GetDownloadStatus().Percent);
            }
            
            if (downloadHandle.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Error while downloading catalog dependencies");
            
            if (downloadHandle.IsValid())
                Addressables.Release(downloadHandle);
        }

        private async UniTask<Dialogue> LoadDialogue(IList<IResourceLocation> locations)
        {
            var localization = new List<TextAsset>();
            DialogueGraphContainer dialogueGraph = null;

            foreach (var location in locations)
            {
                var asset = await _assets.Load<Object>(location.PrimaryKey);

                if (asset is DialogueGraphContainer dialogueGraphContainer)
                    dialogueGraph = dialogueGraphContainer;
                
                if (asset is TextAsset text)
                    localization.Add(text);
            }

            return _adapter.GetDialogueFrom(dialogueGraph, localization);
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