using System;
using Nadsat.DialogueGraph.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Features.Calls.Configs
{
    [Serializable]
    public class CallConfig
    {
        public AssetReferenceT<DialogueGraphContainer> DialogueContainer;
        public AssetReferenceT<TextAsset>[] DialogueContents;
    }
}