using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.Serialization;

namespace CodeBase.Features.Calls.Configs
{
    [Serializable]
    public class LevelConfig 
    {
        public string Name;
        public string DownloadLabel;
        public Sprite Icon;
        public string Description;
        public CallConfig Content;

        public IEnumerable<AssetReference> GetAllReferences()
        {
            yield return Content.DialogueContainer;
            
            foreach (var content in Content.DialogueContents)
                yield return content;
        }
    }
}