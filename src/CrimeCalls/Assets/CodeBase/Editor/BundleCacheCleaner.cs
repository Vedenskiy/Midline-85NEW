using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class BundleCacheCleaner
    {
        [MenuItem("Tools/Clear Addressables bundles cache")]
        private static void ClearBundleCache()
        {
            Caching.ClearCache();
            Debug.Log($"Addressables bundles cache clear.");
        }
    }
}