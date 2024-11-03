using System;
using System.Threading;
using CodeBase.Common.Extensions;
using CodeBase.Infrastructure.Common.AssetManagement.Reports;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.Common.AssetManagement
{
    public class AssetDownloadService
    {
        public async UniTaskVoid InitializeAsync(CancellationToken token = default)
        {
            await Addressables.InitializeAsync().ToUniTask(cancellationToken: token);
            await UpdateCatalogsAsync(token);
        }

        public async UniTask DownloadAsync(string key, AssetDownloadReporter reporter, CancellationToken token = default)
        {
            try
            {
                await DownloadingAsync(key, reporter, token);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public async UniTask<long> GetDownloadSizeAsync(string key, CancellationToken token = default) => 
            await Addressables.GetDownloadSizeAsync(key).ToUniTask(cancellationToken: token);

        private static async UniTask UpdateCatalogsAsync(CancellationToken token = default)
        {
            var catalogsToUpdate = await Addressables
                .CheckForCatalogUpdates()
                .ToUniTask(cancellationToken: token);

            if (catalogsToUpdate.IsNullOrEmpty())
                return;

            await Addressables
                .UpdateCatalogs(catalogsToUpdate)
                .ToUniTask(cancellationToken: token);
        }

        private static async UniTask DownloadingAsync(string key, AssetDownloadReporter reporter, CancellationToken token = default)
        {
            var download = Addressables.DownloadDependenciesAsync(key);

            while (!download.IsDone && download.IsValid())
            {
                await UniTask.Delay(100, cancellationToken: token);
                reporter.Report(download.GetDownloadStatus().Percent);
            }
                
            reporter.Report(1f);
                
            if (download.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Error while downloading catalog dependencies");
                
            if (download.IsValid())
                Addressables.Release(download);
                
            reporter.Reset();
        }
    }
}