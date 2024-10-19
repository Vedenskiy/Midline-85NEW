using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Common.AssetManagement
{
    public class AssetProvider
    {
        public async UniTask<TResource> Load<TResource>(string path) where TResource : Object => 
            await Addressables.LoadAssetAsync<TResource>(path);

        public TResource LoadResource<TResource>(string path) where TResource : Object =>
            Resources.Load<TResource>(path);

        public TResource[] LoadAllResources<TResource>(string path) where TResource : Object =>
            Resources.LoadAll<TResource>(path);
    }
}