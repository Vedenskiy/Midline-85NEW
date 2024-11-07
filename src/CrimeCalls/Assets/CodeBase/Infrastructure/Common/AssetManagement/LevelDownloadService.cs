using System.Collections.Generic;
using System.Text;
using System.Threading;
using CodeBase.Features.Calls;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Menu;
using CodeBase.Infrastructure.Common.AssetManagement.Reports;
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
        private readonly CallRepository _calls;
        private readonly DialogueGraphAdapter _adapter;

        public LevelDownloadService(
            AssetProvider assets, 
            CallRepository calls,
            DialogueGraphAdapter adapter)
        {
            _assets = assets;
            _calls = calls;
            _adapter = adapter;
        }

        public async UniTask<Dialogue> LoadDialogue(string callName, AssetDownloadReporter reporter, CancellationToken token = default)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(callName);
            DebugLocations(locations);
            
            var downloadSize = await Addressables.GetDownloadSizeAsync(locations);
            reporter.Reset();
            reporter.UpdateTargetDownloadSize(downloadSize);
            
            await DownloadDependenciesAsync(locations, reporter, token);
            Debug.Log($"Call {callName} loaded!");

            var dialogue = await LoadDialogue(callName, locations);
            Debug.Log($"Completed dialogue loaded!");
            _calls.Add(callName, dialogue);
            return dialogue;
        }
        
        private async UniTask DownloadDependenciesAsync(IList<IResourceLocation> locations, AssetDownloadReporter reporter, CancellationToken token = default)
        {
            var downloadHandle = Addressables.DownloadDependenciesAsync(locations);
            while (!downloadHandle.IsDone && downloadHandle.IsValid())
            {
                await UniTask.Delay(100, cancellationToken: token);
                reporter.Report(downloadHandle.GetDownloadStatus().Percent);
            }
            
            if (downloadHandle.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Error while downloading catalog dependencies");
            
            if (downloadHandle.IsValid())
                Addressables.Release(downloadHandle);
        }

        private async UniTask<Dialogue> LoadDialogue(string levelName, IList<IResourceLocation> locations)
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

            return _adapter.GetDialogueFrom(dialogueGraph, levelName, localization);
        }

        private void DebugLocations(IEnumerable<IResourceLocation> locations)
        {
            var builder = new StringBuilder();
            foreach (var location in locations) 
                builder.Append($"{location};");
            Debug.Log($"Locations: {builder}");
        }

    }
}