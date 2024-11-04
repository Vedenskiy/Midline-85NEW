using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace CodeBase.Features.Calls.Configs
{
    [Serializable]
    public class LevelConfig 
    {
        public string Name;
        public string DownloadLabel;
        public Sprite Icon;
        public string Description;
        public CallConfig Test;

        public IEnumerable<AssetReference> GetAllReferences()
        {
            yield return Test.DialogueContainer;
            
            foreach (var content in Test.DialogueContents)
                yield return content;
        }
    }
}