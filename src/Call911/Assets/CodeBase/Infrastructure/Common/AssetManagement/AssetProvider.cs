using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Common.AssetManagement
{
    public class AssetProvider
    {
        public async UniTask<TResource> Load<TResource>(string path) where TResource : Object => 
            await Addressables.LoadAssetAsync<TResource>(path);
    }
}