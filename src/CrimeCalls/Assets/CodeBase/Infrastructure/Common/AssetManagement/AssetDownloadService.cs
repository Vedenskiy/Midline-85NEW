using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Common.Extensions;
using CodeBase.Infrastructure.Common.AssetManagement.Reports;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace CodeBase.Infrastructure.Common.AssetManagement
{
    public class AssetDownloadService
    {
        private readonly AssetDownloadReporter _downloadReporter;

        private List<IResourceLocator> _catalogLocators;
        private long _downloadSize;

        public AssetDownloadService(AssetDownloadReporter downloadReporter) => 
            _downloadReporter = downloadReporter;

        public async UniTask InitializeDownloadDataAsync(CancellationToken token = default)
        {
            await Addressables.InitializeAsync().ToUniTask(cancellationToken: token);
            await UpdateCatalogsAsync(token);
            await UpdateDownloadSizeAsync(token);
        }

        public float GetDownloadSizeMb() => 
            SizeToMb(_downloadSize);

        public async UniTask UpdateContentAsync(CancellationToken token = default)
        {
            if (_catalogLocators == null)
                await UpdateCatalogsAsync(token);

            var locations = await RefreshResourceLocations(_catalogLocators, token);

            if (locations.IsNullOrEmpty())
                return;

            try
            {
                await DownloadContentWithPreciseProgress(locations, token);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private async UniTask DownloadContentWithPreciseProgress(IList<IResourceLocation> locations, CancellationToken token)
        {
            var downloadHandle = Addressables.DownloadDependenciesAsync(locations);

            while (!downloadHandle.IsDone && downloadHandle.IsValid())
            {
                await UniTask.Delay(100, cancellationToken: token);
                _downloadReporter.Report(downloadHandle.GetDownloadStatus().Percent);
            }
            
            _downloadReporter.Report(1);
            
            if (downloadHandle.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Error while downloading catalog dependencies");
            
            if (downloadHandle.IsValid())
                Addressables.Release(downloadHandle);
            
            _downloadReporter.Reset();
        }

        private async Task UpdateCatalogsAsync(CancellationToken token)
        {
            var catalogsToUpdate = await Addressables
                .CheckForCatalogUpdates()
                .ToUniTask(cancellationToken: token);

            if (catalogsToUpdate.IsNullOrEmpty())
            {
                _catalogLocators = Addressables.ResourceLocators.ToList();
                return;
            }

            _catalogLocators = await Addressables
                .UpdateCatalogs(catalogsToUpdate)
                .ToUniTask(cancellationToken: token);
        }
        
        private async Task UpdateDownloadSizeAsync(CancellationToken token)
        {
            var locations = await RefreshResourceLocations(_catalogLocators, token);

            if (locations.IsNullOrEmpty())
                return;

            _downloadSize = await Addressables
                .GetDownloadSizeAsync(locations)
                .ToUniTask(cancellationToken: token);
        }

        private static async UniTask<IList<IResourceLocation>> RefreshResourceLocations(IEnumerable<IResourceLocator> locators, CancellationToken token)
        {
            var keysToCheck = locators.SelectMany(x => x.Keys);

            return await Addressables
                .LoadResourceLocationsAsync(keysToCheck, Addressables.MergeMode.Union)
                .ToUniTask(cancellationToken: token);
        }

        private static float SizeToMb(long downloadSize) => downloadSize * 1f / 1048576;
    }
}